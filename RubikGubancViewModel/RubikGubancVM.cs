using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubikGubancModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RubikGubancViewModel
{
    public class RubikGubancVM : INotifyPropertyChanged
    {
        private Game game;

        public event PropertyChangedEventHandler PropertyChanged;


        //  EGY DIMENZIÓS MEGOLDÁS
        /*public string[] ImageOrder
        {
            get
            {
                string[] imageURLs = new string[Game.cardCount];
                for (int i = 0; i < Game.cardCount; i++)
                {
                    imageURLs[i] = game.Cards[i].WhichSide ? game.Cards[i].ElejeImgURL : game.Cards[i].HatuljaImgURL;
                }
                return imageURLs;
            }
        }*/
        //  KÉT DIMENZIÓS MEGOLDÁS
        public List<List<string>> ImageOrder
        {
            get
            {
                List<List<string>> imageURLs = new List<List<string>>();
                int cardIndex = 0;
                for (int i = 0; i < 3; i++)
                {
                    imageURLs.Add(new List<string>());
                    for (int j = 0; j < 3; j++)
                    {
                        //  KELL: kitalálni, hogy kéne a rotationt kezelni --> ez a megoldás nem biztos, hogy jó mert így 1 dologhoz lehetne Bindingelni mindent, ami nem jó
                        //  A kártya képe + hogy mennyivel kell elforgatni
                        //  imageURLs[i].Add((game.Cards[cardIndex].WhichSide ? game.Cards[cardIndex++].ElejeImgURL : game.Cards[cardIndex++].HatuljaImgURL) + " " + game.Cards[cardIndex++].Rotation);

                        //  A kártya képe csak
                        imageURLs[i].Add(game.Cards[cardIndex].WhichSide ? game.Cards[cardIndex++].ElejeImgURL : game.Cards[cardIndex++].HatuljaImgURL);
                    }
                }
                return imageURLs;
            }
        }

        public RubikGubancVM()
        {
            game = new Game();

        }
        public void SetRandomOrder()
        {
            Kevero k = new Kevero();
            foreach (Card card in game.Cards)
            {
                card.WhichSide = Game.rnd.Next(0, 2) == 1 ? true : false;   //  Random kiválasztja, hogy melyik az aktív oldal
                card.Rotation = Game.rnd.Next(0, 4);                        //  Random beállítja a forgási számot
            }
            game.Cards = game.Cards.OrderBy(x => x, k).ToArray();           //  Random szám alapján dönti el, hogy melyik a jobb, és így rendezi, tehát random sorrendje lesz a kártyáknak
            OnPropertyChanged("ImageOrder");
        }

        Card[] backTrackResult = new Card[Game.cardCount];
        bool solved = false;
        public void SolveOne()
        {
            solved = false;
            backTrackResult = new Card[Game.cardCount];
            BackTrack(0, backTrackResult, ref solved);
            if (solved)
            {
                game.Cards = backTrackResult;
            }
            else
            {
                //  KELL: ha nem volt megoldás, akkor kitalálni mit csináljon
            }
            OnPropertyChanged("ImageOrder");
        }

        public void SolveTwo()
        {

            OnPropertyChanged("ImageOrder");
        }

        void BackTrack(int level, Card[] results, ref bool solved)     //  KELL a megvalósítás
        {
            /*  Fk-ban: Mindig meg kell vizsgálni az összes körülötte levő kártyát: ez max. 4 lehet, és min. 2 a széleken, itt azt is meg kell, hogy szélen vagyunk-e
             *  Mszint: A lehetséges részmegoldások a száma a megadott szinten: hány db kártyát hányféleképpen próbálhatunk oda: mindenhol 18*4 elvileg, de lehet, hogy a korábbiakat ki kéne venni
             *  9 szint van
             *  N: 9 részfeladat
             *  Results: lehet, hogy 2 dimenziós tömb kéne
             */
            int i = -1;
            while (!solved && i < Game.cardCount - 1)                    //  kártyák
            {
                i++;
                if (IsPossibleCard(level, game.Cards[i]))                //  Ft függvény, megvizsgálja, hogy az adott kártya benne van-e már a megoldások között
                {                                                        //  Csak akkor megyünk a kártya probálgatásra, ha 
                    int j = -1;
                    while (!solved && j < 1)             //  kártyák oldalai
                    {
                        j++;
                        int k = -1;
                        while (!solved && k < 3)         //  kártyák forgatása
                        {
                            k++;
                            if (IsSuitableCard(level, game.Cards[i], results))
                            {
                                results[level] = game.Cards[i];
                                if (level == Game.cardCount - 1)
                                {
                                    solved = true;
                                }
                                else
                                {
                                    BackTrack(level + 1, results, ref solved);
                                    if(!solved)
                                        results[level] = null;
                                }
                            }
                            if (!solved)       //  ha még nincs megoldás, továbbforgatjuk a kártyát
                            {
                                game.Cards[i].Rotation = (game.Cards[i].Rotation + 1) % 4;  //  mindig forgatjuk tovább, de figyelni kell hogy ne lépjünk túl 3-on
                            }
                        }
                        if (!solved)       //  ha még nincs megoldás, átfordítjuk a kártyát
                        {
                            game.Cards[i].WhichSide = !game.Cards[i].WhichSide;             //  ha megnéztük a forgatást, megnézzük a kártya másik oldalán is ugyanazt
                        }
                    }
                }
            }
        }

        bool IsPossibleCard(int level, Card testedResult)
        {
            return !backTrackResult.Contains(testedResult);       //  ha már szerepel a kártya az eddig berakottak között, akkor semmiképp nem jó ez
        }

        bool IsSuitableCard(int level, Card testedResult, Card[] results)    //  KELL az egész: ha jól ütközik a szomszédaival, akkor return true
        {   //  Csak balra és fel kell vizsgálni, mert jobbra és le még biztos, hogy nincsen kártya
            bool notSuitable = false;
            switch (testedResult.Rotation)
            {
                case 0: notSuitable = !CardRotationCheck(level, testedResult, results, 3, 1, 1, 2);     //  A CardRotationCheck megvizsgálja a balra levő, és a felette levő kártyákat, és ha jó akkor igazzal tér vissza
                    break;
                case 1: notSuitable = !CardRotationCheck(level, testedResult, results, 0, 2, 3, 1);
                    break;
                case 2: notSuitable = !CardRotationCheck(level, testedResult, results, 0, 3, 0, 2);
                    break;
                case 3: notSuitable = !CardRotationCheck(level, testedResult, results, 1, 2, 0, 3);
                    break;
                //  talán default-nak egy Exception, mivel akkor nem jó a rotation értéke
            }

            return !notSuitable;
        }

        bool CardRotationCheck(int level, Card testedResult, Card[] results, int bx, int by, int fx, int fy)    //  Balra és Fel irányba melyik Stringeket kell vizsgáljuk: bx, by, fx, fy
        {
            bool stillGood = true;
            //  BALRA
            int testedResultSide = testedResult.WhichSide ? 1 : 0;  //  A tesztelt kártya aktív oldalát intben elmentem
            if (level != 0 && level != 3 && level != 6)  //  Ezeknél az eseteknél nem kell vizsgálnunk a balra oldalt, mert nincs ott kártya
            {
                Card cardTemp = results[level - 1];         //  A hivatkozások egyszerűsítése miatt
                int cardTempSide = cardTemp.WhichSide ? 1 : 0;     //  A vizsgált szomszéd aktív oldalát intben elmentem
                StringColor bxColor = testedResult.Colors[testedResultSide, bx];
                StringColor byColor = testedResult.Colors[testedResultSide, by];
                switch (cardTemp.Rotation)          //  Megnézzük a vizsgált szomszéd jelenlegi állását
                {
                    case 0: if (bxColor != cardTemp.Colors[cardTempSide, 3] || byColor != cardTemp.Colors[cardTempSide, 0])
                        { stillGood = false; }  //  ha valamelyik string nem jól kapcsolódik, akkor már nem jó a kártya
                        break;
                    case 1: if (bxColor != cardTemp.Colors[cardTempSide, 2] || byColor != cardTemp.Colors[cardTempSide, 1])
                        { stillGood = false; }
                        break;
                    case 2: if (bxColor != cardTemp.Colors[cardTempSide, 1] || byColor != cardTemp.Colors[cardTempSide, 3])
                        { stillGood = false; }
                        break;
                    case 3: if (bxColor != cardTemp.Colors[cardTempSide, 2] || byColor != cardTemp.Colors[cardTempSide, 0])
                        { stillGood = false; }
                        break;
                    //  talán default-nak egy Exception, mivel akkor nem jó a rotation értéke
                }
            }

            //  FEL
            if (stillGood && level > 2)   //  Ha még lehet ez a kártya, és a szint nagyobb 2-nél akkor kell a felfelé irányt vizsgálnunk
            {
                Card cardTemp = results[level - 3];     //  A hivatkozások egyszerűsítése miatt
                int oldal = cardTemp.WhichSide ? 1 : 0;
                StringColor fxColor = testedResult.Colors[testedResultSide, fx];
                StringColor fyColor = testedResult.Colors[testedResultSide, fy];
                switch (cardTemp.Rotation)
                {
                    case 0: if (fxColor != cardTemp.Colors[oldal, 2] || fyColor != cardTemp.Colors[oldal, 0])
                        { stillGood = false; }
                        break;
                    case 1: if (fxColor != cardTemp.Colors[oldal, 3] || fyColor != cardTemp.Colors[oldal, 0])
                        { stillGood = false; }
                        break;
                    case 2: if (fxColor != cardTemp.Colors[oldal, 2] || fyColor != cardTemp.Colors[oldal, 1])
                        { stillGood = false; }
                        break;
                    case 3: if (fxColor != cardTemp.Colors[oldal, 1] || fyColor != cardTemp.Colors[oldal, 3])
                        { stillGood = false; }
                        break;
                }
            }
            return stillGood;
        }

        void OnPropertyChanged([CallerMemberName]string n = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(n));
        }
    }
}
