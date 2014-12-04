using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class LeaderBoardEntry
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Pushes { get; set; }
    }
}
