using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class GameResultsDto
    {
        private List<PlayerResultsDto> _results = new List<PlayerResultsDto>(); 
        public IEnumerable<PlayerResultsDto> Results { get { return _results; } }

        public GameResultsDto(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                _results.Add(new PlayerResultsDto(player));
            }
        }
    }
}
