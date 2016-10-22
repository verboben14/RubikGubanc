using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikGubancModel
{
    public struct CardDescription      //  Ebben a struktúrában eltárolom a kártyák kiválasztott képét (hátulja, vagy eleje), és a forgatás mértékét fokban (0, 90, 180, 270)
    {
        public string URL { get; set; }
        public int Rotation { get; set; }
    }

    public enum StringColor
    {
        Blue, Green, Yellow, Red    //  Színeket tartalmazó felsorolás
    }

    public class Card
    {
        public const int CardSideCount = 2;
        public const int CardColorCount = 4;
        StringColor[,] colors;  //  az első dimenzió határozza meg, hogy melyik oldala a kártyának
        //  második dimenzió: mivel minden kártya ugyanúgy néz ki, ezért felállíthatunk egy sorrendet a madzagok pozíciója alapján, és ezeknek a színét eltárolhatjuk egy tömbben
        bool whichSide;         //  melyik az éppen aktív oldal
        string elejeImgURL;     //  kártya elejéhez tartozó kép elérési útja
        string hatuljaImgURL;   //  kártya hátuljához tartozó kép elérési útja
        int rotation;           //  kártya aktuális forgatási száma

        public StringColor[,] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public bool WhichSide
        {
            get { return whichSide; }
            set { whichSide = value; }
        }

        public string ElejeImgURL
        {
            get { return elejeImgURL; }
            set { elejeImgURL = value; }
        }

        public string HatuljaImgURL
        {
            get { return hatuljaImgURL; }
            set { hatuljaImgURL = value; }
        }
        public int Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public int GetRotationDegree
        {
            get { return rotation * 90; }   //  mivel mindig 90 fok többszörösével forgatunk, ezért a 0, 1, 2, 3 értékeket szorozzuk 90-nel => 0, 90, 180, 270 fokok keletkezhetnek
        }

        public Card()
        {
            whichSide = true;
        }
    }

    public class Game
    {
        public const int cardCount = 9;             //  9 kártya van a játékban
        public static Random rnd = new Random();    //  Random szám generátor, több helyen is használom
        Card[] cards;                               //  A kártyákat tartalmazó tömb

        public Card[] Cards
        {
            get { return cards; }
            set { cards = value; }
        }

        public Game()
        {
            cards = new Card[cardCount];
            for (int i = 0; i < cardCount; i++)
            {
                cards[i] = new Card();
            }
            SetColors(cards);
            SetImageURLs(cards);
        }

        void SetColors(Card[] cards)    // Feltölti minden kártya színeit a játéknak megfelelő színekkel
        {
            cards[0].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Green, StringColor.Yellow, StringColor.Blue, StringColor.Red },      //  Kártya egyik oldala
                                                                                         { StringColor.Blue, StringColor.Yellow, StringColor.Green, StringColor.Red } };    //  Kártya másik oldala
            cards[1].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Green, StringColor.Yellow, StringColor.Red, StringColor.Blue },
                                                                                         { StringColor.Yellow, StringColor.Green, StringColor.Red, StringColor.Blue } };
            cards[2].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Green, StringColor.Blue, StringColor.Yellow, StringColor.Red },
                                                                                         { StringColor.Yellow , StringColor.Green , StringColor.Blue , StringColor.Red } };
            cards[3].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Yellow , StringColor.Blue , StringColor.Green  , StringColor.Red },
                                                                                         { StringColor.Blue, StringColor.Green , StringColor.Yellow , StringColor.Red } };
            cards[4].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Yellow , StringColor.Red , StringColor.Blue, StringColor.Green  },
                                                                                         { StringColor.Blue, StringColor.Yellow, StringColor.Red , StringColor.Green  } };
            cards[5].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Yellow , StringColor.Red , StringColor.Green , StringColor.Blue  },
                                                                                         { StringColor.Red , StringColor.Green , StringColor.Yellow , StringColor.Blue  } };
            cards[6].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Red , StringColor.Yellow, StringColor.Blue, StringColor.Green  },
                                                                                         { StringColor.Blue, StringColor.Red , StringColor.Yellow , StringColor.Green  } };
            cards[7].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Red , StringColor.Blue , StringColor.Yellow , StringColor.Green  },
                                                                                         { StringColor.Yellow , StringColor.Green , StringColor.Red , StringColor.Blue  } };
            cards[8].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Red , StringColor.Yellow, StringColor.Green , StringColor.Blue  },
                                                                                         { StringColor.Green , StringColor.Red , StringColor.Yellow , StringColor.Blue } };
        }

        void SetImageURLs(Card[] cards)     //  Beállítja minden kártya esetében a kártya hátuljának és elejének képének URL-jét
        {
            for (int i = 0; i < cardCount; i++)
            {
                cards[i].ElejeImgURL = "\\Images\\_" + (i + 1) + "Eleje.png";
                cards[i].HatuljaImgURL = "\\Images\\_" + (i + 1) + "Hatulja.png";
            }
        }
    }

    public class Shuffler : IComparer<object>       //  A keverést segítő osztály, két object paraméter közül random szám alapján visszaadja a "nagyobbat"
    {
        public int Compare(object x, object y)
        {
            return Game.rnd.Next(-1, 2);
        }
    }

    public class MyException : Exception            //  Saját kivétel, konstruktora átadja a paraméterként kapott üzenetet az ősosztály konstruktorának
    {
        public MyException(string uzenet)
            : base(uzenet)
        { }
    }
}
