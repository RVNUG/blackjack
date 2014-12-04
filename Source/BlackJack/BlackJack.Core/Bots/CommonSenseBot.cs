using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core.Bots
{
    public class CommonSenseBot: IPlayerBot
    {
        string IPlayerBot.RegisterBot(int playerPosition)
        {
            return "Common Sense";
        }

        void IPlayerBot.CardDealt(int playerPosition, CardDto card)
        {
        }

        PlayerAction IPlayerBot.PlayTurn(TurnStatus status)
        {
            var playerTotal = status.PlayerHand.HandTotal;
            var dealerTotal = status.DealerHand.HandTotal;

            // won't know the actual dealer total at this point, only know what card he is showing.

            if (playerTotal <= 11)
            {
                return PlayerAction.Hit;
            }

            // if the dealer is showing a low card we want him to bust
            if (dealerTotal < 5)
            {
                return PlayerAction.Stand;
            }

            if (playerTotal - 10 < dealerTotal && playerTotal < 17)
            {
                return PlayerAction.Hit;
            }

            if (playerTotal > 17)
            {
                return PlayerAction.Stand;
            }

            return status.PlayerHand.ContainsCard(FaceValue.Ace) ? 
                PlayerAction.Hit : 
                PlayerAction.Stand;
        }

        void IPlayerBot.RoundComplete(RoundResult result)
        {
        }


        void IPlayerBot.CardsShuffled()
        {
        }
    }
}