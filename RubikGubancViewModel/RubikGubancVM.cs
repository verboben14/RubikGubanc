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

        public Array ImageOrder
        {
            get
            {
                string[] ImageURLs = new string[Game.cardCount];
                for (int i = 0; i < Game.cardCount; i++)
                {
                    ImageURLs[i] = game.Cards[i].WhichSide ? game.Cards[i].ElejeImgURL : game.Cards[i].HatuljaImgURL;
                }
                return ImageURLs;
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

        

        void OnPropertyChanged([CallerMemberName]string n="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
        }
    }
}
