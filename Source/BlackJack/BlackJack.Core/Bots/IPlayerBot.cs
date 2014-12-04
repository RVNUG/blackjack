namespace BlackJack.Core.Bots
{
    public interface IPlayerBot
    {
        string RegisterBot(int playerPosition);

        void CardDealt(int playerPosition, CardDto card);
        PlayerAction PlayTurn(TurnStatus status);
        void RoundComplete(RoundResult result);

        void CardsShuffled();
    }
}
