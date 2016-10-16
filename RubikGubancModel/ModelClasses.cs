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
        public const int CardSideCount = 2;
        public const int CardColorCount = 4;
        string elejeImgURL;
        string hatuljaImgURL;
        bool whichSide;     // melyik az éppen vizsgált, aktív oldal
        StringColor[,] colors;  // az első dimenzió határozza meg, hogy melyik oldala a kártyának
        // mivel minden kártya ugyanúgy néz ki, ezért felállíthatunk egy sorrendet a madzagok pozíciója alapján, és ezeknek a színét eltárolhatjuk egy tömbben
        int x, y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

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

        public Card()
        {
            whichSide = true;
        }

        public override string ToString()
        {
            return whichSide ? Colors[1, 0].ToString() + "\n" + Colors[1, 1].ToString() + "\n" + Colors[1, 2].ToString() + "\n" + Colors[1, 3].ToString() :
                               Colors[0, 0].ToString() + "\n" + Colors[0, 1].ToString() + "\n" + Colors[0, 2].ToString() + "\n" + Colors[0, 3].ToString();
        }
    }

    public class Game
    {
        public const int cardCount = 9;
        public static Random rnd = new Random();
        Card[] cards;
        public Card[] Cards
        {
            get { return cards; }
            set { cards = value; }
        }
        public Game()
        {
            cards = new Card[cardCount];    // 9 kártyánk van a játékban
            for (int i = 0; i < cardCount; i++)
            {
                cards[i] = new Card();
            }
            SetColors(cards);
            SetImageURLs(cards);
        }

        void SetColors(Card[] cards)    // Feltölti a kapott cards tömböt a játékban szereplő színekkel
        {
            /*  KELL: CIKLUSSAL FELTÖLTÉS
             * for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < Card.CardSideCount; j++)
                {
                    for (int k = 0; k < Card.CardColorCount; k++)
                    {
                        cards[i].Colors
                    }
                }
            }*/
            cards[0].Colors = new StringColor[Card.CardSideCount, Card.CardColorCount] { { StringColor.Green, StringColor.Yellow, StringColor.Blue, StringColor.Red },      //Első oldal
                                                                                         { StringColor.Blue, StringColor.Yellow, StringColor.Green, StringColor.Red } };    //Második oldal
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
        void SetImageURLs(Card[] cards)
        {
            cards[0].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\elsoEleje.png";
            cards[0].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\elsoHatulja.png";
            cards[1].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\masodikEleje.png";
            cards[1].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\masodikHatulja.png";
            cards[2].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\harmadikEleje.png";
            cards[2].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\harmadikHatulja.png";
            cards[3].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\negyedikEleje.png";
            cards[3].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\negyedikHatulja.png";
            cards[4].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\otodikEleje.png";
            cards[4].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\otodikHatulja.png";
            cards[5].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\hatodikEleje.png";
            cards[5].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\hatodikHatulja.png";
            cards[6].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\hetedikEleje.png";
            cards[6].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\hetedikHatulja.png";
            cards[7].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\nyolcadikEleje.png";
            cards[7].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\nyolcadikHatulja.png";
            cards[8].ElejeImgURL = "D:\\Repos\\RubikGubanc\\Sources\\kilencedikEleje.png";
            cards[8].HatuljaImgURL = "D:\\Repos\\RubikGubanc\\Sources\\kilencedikHatulja.png";
        }
    }

    public class Kevero : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            return Game.rnd.Next(-1, 2);
        }
    }
}
