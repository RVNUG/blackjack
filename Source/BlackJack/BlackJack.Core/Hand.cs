using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BlackJack.Core
{
    public class Hand
    {
        // Creates a list of cards
        protected List<Card> cards = new List<Card>();
        public int NumCards { get { return cards.Count; } }
		public List<Card> Cards { get { return cards; } }

        /// <summary>
        /// Checks to see if the hand contains a card of a certain face value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsCard(FaceValue item)
        {
            return cards.Any(c => c.FaceVal == item);
        }
    }

    /// <summary>
    /// This class is game-specific.  Creates a BlackJack hand that inherits from class hand
    /// </summary>
    public class BlackJackHand : Hand
    {
        /// <summary>
        /// This method compares two BlackJack hands
        /// </summary>
        /// <param name="otherHand"></param>
        /// <returns></returns>
        public int CompareFaceValue(object otherHand)
        {
            var aHand = otherHand as BlackJackHand;
            if (aHand != null)
            {
                return GetSumOfHand().CompareTo(aHand.GetSumOfHand());
            }
            throw new ArgumentException("Argument is not a Hand");
        }

        /// <summary>
        /// Gets the total value of a hand from BlackJack values
        /// </summary>
        /// <returns>int</returns>
        public int GetSumOfHand(bool includeFaceDownCards = true)
        {
            int val = 0;
            int numAces = 0;

            foreach (Card c in cards)
            {
                if (!c.IsCardUp)
                {
                    continue;
                }

                switch (c.FaceVal)
                {
                    case FaceValue.Ace:
                        numAces++;
                        val += 11;
                        break;
                    case FaceValue.King:
                    case FaceValue.Queen:
                    case FaceValue.Jack:
                        val += 10;
                        break;
                    default:
                        val += (int)c.FaceVal;
                        break;
                }
            }

            while (val > 21 && numAces > 0)
            {
                val -= 10;
                numAces--;
            }

            return val;
        }

        public override string ToString()
        {
            return GetSumOfHand().ToString(CultureInfo.InvariantCulture);
        }
    }
}
