using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class CardDto
    {
        public readonly FaceValue? FaceVal = null;
        public readonly Suit? Suit = null;

        public CardDto(Card card)
        {
            if (card.IsCardUp)
            {
                FaceVal = card.FaceVal;
                Suit = card.Suit;
            }
        }
    }
}
