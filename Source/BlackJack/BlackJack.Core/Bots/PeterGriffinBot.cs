using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core.Bots
{
    public class PeterGriffinBot: IPlayerBot
    {
        public string RegisterBot(int playerPosition)
        {
            return "Peter Griffin";
        }

        public void CardDealt(int playerPosition, CardDto card)
        {
        }

        public PlayerAction PlayTurn(TurnStatus status)
        {
            return PlayerAction.Hit;
        }

        public void RoundComplete(RoundResult result)
        {            
        }

        public void CardsShuffled()
        {
        }
    }
}
