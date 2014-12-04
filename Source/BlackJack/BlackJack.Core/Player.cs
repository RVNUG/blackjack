using BlackJack.Core.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public IPlayerBot Bot { get; private set; }

        public BlackJackHand Hand { get; private set; }

        public int Wins { get; private set; }

        public int Losses { get; private set; }

        public int Pushes { get; private set; }

        public Player(int playerIndex, IPlayerBot bot)
        {
            Bot = bot;
            Position = playerIndex;
            Name = bot.RegisterBot(playerIndex);

            Hand = new BlackJackHand();
            Wins = 0;
            Losses = 0;
            Pushes = 0;
        }

        public void ResetHand()
        {
            Hand = new BlackJackHand();
        }

        public void AddWin()
        {
            Wins++;
        }

        public void AddLoss()
        {
            Losses++;
        }

        public void AddPush()
        {
            Pushes++;
        }

        public override string ToString()
        {
            return Bot.GetType() + ": " + Hand;
        }
    }
}