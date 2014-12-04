using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class PlayerResultsDto
    {
        public string PlayerName;
        public int Wins;
        public int Losses;
        public int Pushes;

        public PlayerResultsDto(Player player)
        {
            PlayerName = player.Name;
            Wins = player.Wins;
            Losses = player.Losses;
            Pushes = player.Pushes;
        }
    }
}
