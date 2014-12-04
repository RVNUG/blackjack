using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class BlackJackHandDto
    {
        public readonly int HandTotal;
        public readonly IEnumerable<CardDto> Cards; 

        public BlackJackHandDto(BlackJackHand hand)
        {
            HandTotal = hand.GetSumOfHand(false);

            var cards = new List<CardDto>(hand.Cards.Count);
            cards.AddRange(hand.Cards.Select(card => new CardDto(card)));
            Cards = cards;
        }

        /// <summary>
        /// Checks to see if the hand contains a card of a certain face value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsCard(FaceValue item)
        {
            return Cards.Any(c => c.FaceVal == item);
        }
    }
}
