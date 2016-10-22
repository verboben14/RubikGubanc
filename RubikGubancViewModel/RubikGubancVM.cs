using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubikGubancModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace RubikGubancViewModel
{
    public class RubikGubancVM : INotifyPropertyChanged
    {
        private Game game;

        public event PropertyChangedEventHandler PropertyChanged;       //  A tulajdonságok változásához kapcsolódó esemény

        public bool Solved
        {
            get { return solved; }
            set { solved = value; }
        }

        public List<List<CardDescription>> ImageOrder      //  Ez a lista fogja tartalmazni az éppen aktuális sorrendjét a kártyáknak két dimenziósan a megjelenítés miatt, elemeiben a kép URL-jét és a kártya forgatását is
        {
            get
            {
                List<List<CardDescription>> imageDescriptions = new List<List<CardDescription>>();
                int sqrtCardCount = (int)(Math.Sqrt(Game.cardCount));
                for (int i = 0; i < sqrtCardCount; i++)
                {
                    imageDescriptions.Add(new List<CardDescription>());    //  A második dimenzió
                    for (int j = 0; j < sqrtCardCount; j++)
                    {
                        //  Hozzáadom a megfelelő kártya adatait tartalmazó struktúrát a listához
                        try
                        {
                            imageDescriptions[i].Add(new CardDescription() { URL = game.Cards[i * sqrtCardCount + j].WhichSide ? game.Cards[i * sqrtCardCount + j].HatuljaImgURL : game.Cards[i * sqrtCardCount + j].ElejeImgURL, Rotation = game.Cards[i * sqrtCardCount + j].GetRotationDegree });
                        }
                        catch(IndexOutOfRangeException)
                        {
                            Debug.WriteLine("Kártya leírások feltöltése: Az index a tömb határain kívűlre mutatott!");
                        }
                    }
                }
                return imageDescriptions;
            }
        }

        public RubikGubancVM()
        {
            game = new Game();
            SetRandomOrder();
        }

        public void SetRandomOrder()    //  Összekveri a kártyákat: sorrendet, oldalt és forgatási számot állít random
        {
            Shuffler k = new Shuffler();        //  Saját keverő osztály, ami random dönti el, hogy melyik paraméter a nagyobb
            foreach (Card card in game.Cards)
            {
                card.WhichSide = Game.rnd.Next(0, 2) == 1 ? true : false;   //  Random kiválasztja, hogy melyik az aktív oldal
                card.Rotation = Game.rnd.Next(0, 4);                        //  Random beállítja a forgási számot
            }
            game.Cards = game.Cards.OrderBy(x => x, k).ToArray();           //  "Rendezem" a keverő példány segítségével
            OnPropertyChanged("ImageOrder");                                //  Megváltozott a lista, tehát meghívom az eseményt kiváltó metódust
            solved = false;
            OnPropertyChanged("Solved");                                    //  Keverés után nincs megoldva, tehát erre is meghívjuk a metódust
        }

        //  A játék megoldása
        Card[] backTrackResult = new Card[Game.cardCount];          //  Ebben a tömbben lesz a végső megoldás, és közben a részmegoldások is
        bool solved = false;

        public void Solve(ref string hiba)
        {
            solved = false;
            backTrackResult = new Card[Game.cardCount];
            try
            {
                BackTrackSolve(0, backTrackResult, ref solved);
            }
            catch (MyException e)
            {
                hiba = e.Message;
            }
            catch (IndexOutOfRangeException)
            {
                hiba = "Az index a tömb határain kívülre mutatott!";
            }
            catch (Exception)
            {
                hiba = "Hiba történt!";
            }
            if (solved)
            {
                game.Cards = backTrackResult;
            }
            OnPropertyChanged("ImageOrder");
            OnPropertyChanged("Solved");
        }

        void BackTrackSolve(int level, Card[] results, ref bool solved)
        {
            int i = -1;
            while (!solved && i < Game.cardCount - 1)                    //  Végigmegy a kártyákon
            {
                i++;
                if (IsPossibleCard(game.Cards[i]))                //  Megvizsgálja, hogy az adott kártya benne van-e már a results tömbben, ha igen nem rakhatjuk bele mégegyszer
                {                                                        //  Csak akkor megyünk a kártya próbálgatásra, ha még lehetséges ez a kártya
                    int j = -1;
                    while (!solved && j < Card.CardSideCount - 1)              //  Megfordítja a kártyát (eleje, hátulja)
                    {
                        j++;
                        int k = -1;
                        while (!solved && k < Card.CardColorCount - 1)         //  Végigforgatja a kártyát
                        {
                            k++;
                            if (IsSuitableCard(level, game.Cards[i], results))      //  Megvizsgálja, hogy az eddigi részmegoldásokat figyelembe véve megfelelő-e a kártya
                            {
                                results[level] = game.Cards[i];                     //  Ha igen, hozzáadjuk az eddig részmegoldásokhoz
                                if (level == Game.cardCount - 1)                    //  Ha a részmegoldásban megvan minden kártya, akkor megtaláltuk a megoldást
                                {
                                    solved = true;
                                }
                                else
                                {
                                    BackTrackSolve(level + 1, results, ref solved);     //  Meghívjuk eggyel magasabb szintre a metódust
                                    if (!solved)
                                    {
                                        results[level] = null;                      //  Ha nincs megoldva még, és visszatér a rekurzióból, akkor nullra kell állítani az aktuális elemet, mert különben benne maradna a részmegoldások között egy nem jó kártya
                                    }
                                }
                            }
                            if (!solved)       //  Ha még nincs megoldás, továbbforgatjuk a kártyát
                            {
                                game.Cards[i].Rotation = (game.Cards[i].Rotation + 1) % 4;  //  Mindig forgatjuk tovább, de figyelni kell hogy ne lépjünk túl 3-on
                            }
                        }
                        if (!solved)        //  Ha még nincs megoldás, megfordítjuk a kártyát
                        {
                            game.Cards[i].WhichSide = !game.Cards[i].WhichSide;             //  Ha megnéztük a forgatást, megnézzük a kártya másik oldalán is ugyanazt
                        }
                    }
                }
            }
        }

        bool IsPossibleCard(Card testedResult)
        {
            return !backTrackResult.Contains(testedResult);       //  Ha már szerepel a kártya az eddig berakottak között, akkor semmiképp nem jó ez
        }

        bool IsSuitableCard(int level, Card testedResult, Card[] results)
        {   //  Csak balra és fel kell vizsgálni, mert jobbra és le szomszédkártya még biztos, hogy nincsen
            bool notSuitable = false;
            switch (testedResult.Rotation)
            {
                //  Az éppen tesztelt kártya forgatási száma alapján, átadjuk paraméterként, hogy melyik madzagokat kell vizsgálni az egyes szomszédok esetében (utolsó 4 paraméter)
                case 0: notSuitable = !CardRotationCheck(level, testedResult, results, 3, 1, 1, 2);     //  A CardRotationCheck megvizsgálja a balra levő, és a felette levő kártyákat, és ha jó akkor igazzal tér vissza
                    break;
                case 1: notSuitable = !CardRotationCheck(level, testedResult, results, 0, 2, 3, 1);
                    break;
                case 2: notSuitable = !CardRotationCheck(level, testedResult, results, 0, 3, 0, 2);
                    break;
                case 3: notSuitable = !CardRotationCheck(level, testedResult, results, 1, 2, 0, 3);
                    break;
                default:
                    throw new MyException("Nem megfelelő a forgatási szám: " + testedResult.Rotation);        //  Ha ezektől különböző érték, akkor nem jó a forgatási szám
            }
            return !notSuitable;
        }

        bool CardRotationCheck(int level, Card testedResult, Card[] results, int b1, int b2, int f1, int f2)    //  Balra és Fel irányba melyik madzagokat kell vizsgáljuk: b1, b2, f1, f2
        {
            bool stillGood = true;
            //  Vizsgálat a balra szomszédra:
            int testedResultSide = testedResult.WhichSide ? 1 : 0;  //  A tesztelt kártya aktív oldalát intben elmentem az indexelés miatt
            int sqrtCardCount = (int)Math.Sqrt(Game.cardCount);
            if (level % sqrtCardCount != 0)  //  Ezeknél az eseteknél nem kell vizsgálnunk a balra oldalt, mert nincs ott kártya (0-ás, 3-as, 6-os indexű kártyák)
            {
                Card cardTemp = results[level - 1];         //  A hivatkozások egyszerűsítése miatt
                int cardTempSide = cardTemp.WhichSide ? 1 : 0;     //  A vizsgált szomszéd aktív oldalát intben elmentem
                StringColor bxColor = testedResult.Colors[testedResultSide, b1];
                StringColor byColor = testedResult.Colors[testedResultSide, b2];
                switch (cardTemp.Rotation)          //  Megnézzük a vizsgált szomszéd jelenlegi állását
                {
                    case 0: if (bxColor != cardTemp.Colors[cardTempSide, 3] || byColor != cardTemp.Colors[cardTempSide, 0])
                        { stillGood = false; }  //  Ha valamelyik madzag nem jól kapcsolódik, akkor már nem jó a kártya
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
                    default:
                        throw new MyException("Nem megfelelő a forgatási szám: " + cardTemp.Rotation);        //  Ha ezektől különböző érték, akkor nem jó a forgatási szám
                }
            }

            //  Vizsgálat a felső szomszédra, ha balra szomszéd vizsgálat sikerült:
            if (stillGood && level > sqrtCardCount - 1)   //  Ha a szint kisebb kettőnél, akkor nem kell vizsgálnunk a felszomszédot, mivel az az első sor
            {
                Card cardTemp = results[level - sqrtCardCount];     //  A hivatkozások egyszerűsítése miatt
                int oldal = cardTemp.WhichSide ? 1 : 0;
                StringColor fxColor = testedResult.Colors[testedResultSide, f1];
                StringColor fyColor = testedResult.Colors[testedResultSide, f2];
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
                    default:
                        throw new MyException("Nem megfelelő a forgatási szám: + " + cardTemp.Rotation);        //  Ha ezektől különböző érték, akkor nem jó a forgatási szám
                }
            }
            return stillGood;
        }

        //  Az eseményt kiváltó metódus
        void OnPropertyChanged([CallerMemberName]string n = "")     //  A paraméterként kapott tulajdonság név alapján váltja ki az eseményt, ha nem kap, akkor pedig a hívó neve adódik át
        {
            if (PropertyChanged != null)            //  Megvizsgáljuk, hogy az esemény nem null-e
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(n));
        }
    }
}