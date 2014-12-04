using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class RoundResult
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public PlayerResult Result { get; set; }
        public BlackJackHandDto DealerHand { get; set; }
        public BlackJackHandDto PlayerHand { get; set; }
    }
}
