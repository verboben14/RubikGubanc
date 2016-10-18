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

        public void SolveOne()
        {

            OnPropertyChanged("ImageOrder");
        }

        public void SolveTwo()
        {

            OnPropertyChanged("ImageOrder");
        }

        Card[] backTrackResult = new Card[Game.cardCount];
        void BackTrack(int level, Card[] results, ref bool van)     //  KELL a megvalósítás
        {
            /*  Mindig meg kell vizsgálni az összes körülötte levő kártyát: ez max. 4 lehet, és min. 2 a széleken, itt azt is meg kell, hogy szélen vagyunk-e
             *  Mszint: A lehetséges részmegoldások a száma a megadott szinten: hány db kártyát hányféleképpen próbálhatunk oda: mindenhol 18*4 elvileg, de lehet, hogy a korábbiakat ki kéne venni
             *  9 szint van
             *  N: 9 részfeladat
             *  Results: lehet, hogy 2 dimenziós tömb kéne
             */
            int i = -1;
            int resultIndex = 0;
            int mszint = 9;
            while (!van && i < mszint)                          //  kártyák
            {
                i++;
                int j = -1;
                while (!van && j < 2)                           //  kártyák oldalai
                {
                    j++;                                        
                    int k = -1;
                    while (!van && k < 4)                       //  kártyák forgatása
                    {
                        k++;                                    
                        if (LehetEz(level, game.Cards[i]))      //  Ft függvény, megvizsgálja, hogy az adott kártya benne van-e már a megoldások között
                        {
                            if (JoEz(level, game.Cards[i], results))
                            {
                                results[resultIndex++] = game.Cards[i];
                                if (level == Game.cardCount)
                                {
                                    van = true;
                                }
                                else
                                {
                                    BackTrack(level + 1, results, ref van);
                                }
                            }
                        }
                        game.Cards[i].Rotation = (game.Cards[i].Rotation + 1) % 4;  //  mindig forgatjuk tovább, de figyelni kell hogy ne lépjünk túl 3-on
                    }
                    game.Cards[i].WhichSide = !game.Cards[i].WhichSide;             //  ha megnéztük a forgatást, megnézzük a kártya másik oldalán is ugyanazt
                }
            }
        }

        bool LehetEz(int level, Card semiResult)
        {
            return !backTrackResult.Contains(semiResult);       //  ha már szerepel a kártya az eddig berakottak között, akkor semmiképp nem jó ez
        }

        bool JoEz(int level, Card semiResult, Card[] results)
        {
            return true;                                        //  KELL az egész: ha jól ütközik a szomszédaival, akkor return true
        }
        void OnPropertyChanged([CallerMemberName]string n = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(n));
        }
    }
}
