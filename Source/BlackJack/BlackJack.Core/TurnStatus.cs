using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class TurnStatus
    {
        public BlackJackHandDto DealerHand { get; set; }
        public BlackJackHandDto PlayerHand { get; set; }
    }
}
