namespace BlackJack.Core.Bots
{
    public class DealerBot: IPlayerBot
    {
        string IPlayerBot.RegisterBot(int playerPosition)
        {
            return "Dealer";
        }

        void IPlayerBot.CardDealt(int playerPosition, CardDto card)
        {
        }

        PlayerAction IPlayerBot.PlayTurn(TurnStatus status)
        {
            var handTotal = status.PlayerHand.HandTotal;
            
            if (handTotal < 17)
                return PlayerAction.Hit;

            if (handTotal > 17) 
                return PlayerAction.Stand;

            return status.PlayerHand.ContainsCard(FaceValue.Ace) ? 
                PlayerAction.Hit : PlayerAction.Stand;
        }

        void IPlayerBot.RoundComplete(RoundResult result)
        {
        }


        void IPlayerBot.CardsShuffled()
        {
        }
    }
}
