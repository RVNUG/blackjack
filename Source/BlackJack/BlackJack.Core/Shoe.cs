using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class Shoe : Deck
    {
        public Shoe(int numberOfDecks = 6)
        {
            if (numberOfDecks < 1)
            {
                numberOfDecks = 6;
            }

            for (int i = 0; i < numberOfDecks; i++)
            {
                LoadDeck();
            }
        }
    }
}