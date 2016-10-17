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
                int cardIndex=0;
                for (int i = 0; i < 3; i++)
                {
                    imageURLs.Add(new List<string>());
                    for (int j = 0; j < 3; j++)
                    {
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
                card.WhichSide = Game.rnd.Next(0, 2) == 1 ? true : false;
            }
            game.Cards = game.Cards.OrderBy(x => x, k).ToArray();
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



        void OnPropertyChanged([CallerMemberName]string n = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(n));
        }
    }
}
