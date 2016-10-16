using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubikGubancModel;

namespace RubikGubancViewModel
{
    public class RubikGubancVM
    {
        private Game game;


        public Card[] GetRandomOrder()
        {
            // return game.Cards.OrderBy(); // össze kell keverni, és utána visszaadni
            return game.Cards;
        }

        public Card[] GetOrder()
        {
            return game.Cards;
        }
    }
}
