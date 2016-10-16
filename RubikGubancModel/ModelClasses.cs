using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikGubancModel
{
    public enum StringColor
    {
        Blue, Green, Yellow, Red
    }

    public class Card
    {
        bool whichSide;     // melyik az éppen vizsgált, aktív oldal
        StringColor[,] colors;  // az első dimenzió határozza meg, hogy melyik oldala a kártyának
        // mivel minden kártya ugyanúgy néz ki, ezért felállíthatunk egy sorrendet a madzagok pozíciója alapján, és ezeknek a színét eltárolhatjuk egy tömbben

        public Card()
        {
            whichSide = true;
            colors = new StringColor[2, 4];
        }

    }

    public class Game
    {
        Card[] cards;
        public Card[] Cards
        {
            get { return cards; }
        }
        public Game()
        {
            cards = new Card[9];    // 9 kártyánk van a játékban
        }
    }
}
