using BlackJack.Core.Bots;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class GameEngine
    {
        private readonly List<RoundResult> _roundResults = new List<RoundResult>();
        private readonly Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();
        private Shoe _shoe;

        public GameEngine() { }

        public GameEngine(IPlayerBot[] initialPlayers)
        {
            foreach (var player in initialPlayers)
            {
                this.AddPlayerToGame(player);
            }
        }

        public readonly Player Dealer = new Player(-1, new DealerBot());

        public IEnumerable<Player> PlayersWithDealer
        {
            get
            {
                var playersWithDealer = new List<Player>();
                playersWithDealer.AddRange(_players.Values);
                playersWithDealer.Add(Dealer);

                return playersWithDealer;
            }
        }

        public IEnumerable<RoundResult> RoundResults
        {
            get
            {
                return _roundResults.AsEnumerable();
            }
        }

        public Guid AddPlayerToGame(IPlayerBot bot)
        {
            var playerId = Guid.NewGuid();

            var player = new Player(_players.Count, bot)
            {
                Id = playerId
            };
            _players.Add(playerId, player);

            return playerId;
        }

        public Player GetPlayer(Guid id)
        {
            return _players[id];
        }

        public void RunGame()
        {
            _roundResults.Clear();

            if (_players.Count < 1)
            {
                throw new Exception("You must add players before running the game");
            }

            _shoe = new Shoe();
            _shoe.Shuffle();
            NotifyPlayersOfCardShuffled();

            for (int i = 0; i < 100; i++)
            {
                PlayRound();
            }
        }

        internal void PlayRound()
        {
            DealCards();

            PlayTurnsForRound();

            TabulateRoundResults();
        }

        internal void DealCards()
        {
            if (_shoe == null || _shoe.CardsRemaining < 52)
            {
                _shoe = new Shoe();
                _shoe.Shuffle();
                NotifyPlayersOfCardShuffled();
            }

            foreach (var player in PlayersWithDealer)
            {
                player.ResetHand();
            }

            // deal two cards to each player
            for (int cardNumber = 1; cardNumber <= 2; cardNumber++)
            {
                var index = 0;
                foreach (var player in PlayersWithDealer)
                {
                    DealCardToPlayer(index, player);
                    index++;
                }
            }
        }

        private void DealCardToPlayer(int playerIndex, Player player)
        {
            var card = _shoe.Draw();

            if (player == Dealer && player.Hand.NumCards == 0)
            {
                card.IsCardUp = false;
            }

            player.Hand.Cards.Add(card);

            NotifyPlayersOfCardDealt(playerIndex, card);
        }

        private void NotifyPlayersOfCardDealt(int playerPosition, Card card)
        {
            foreach (var player in _players.Values)
            {
                player.Bot.CardDealt(playerPosition, new CardDto(card));
            }
        }

        private void NotifyPlayersOfCardShuffled()
        {
            foreach (var player in _players.Values)
            {
                player.Bot.CardsShuffled();
            }
        }

        internal void PlayTurnsForRound()
        {
            var index = 0;
            foreach (var player in PlayersWithDealer)
            {
                if (player == Dealer)
                {
                    // now the dealer needs to play his turn
                    // first set his card face up
                    Dealer.Hand.Cards[0].IsCardUp = true;
                }

                PlayTurnForPlayer(player, index);
                index++;
            }
        }

        private void PlayTurnForPlayer(Player player, int playerIndex)
        {
            var turnStatus = GetTurnStatus(player);

            if (turnStatus.PlayerHand.HandTotal == 21)
            {
                return;
            }
            
            var action = player.Bot.PlayTurn(turnStatus);

            while (action == PlayerAction.Hit && player.Hand.GetSumOfHand() < 21)
            {
                DealCardToPlayer(playerIndex, player);
                turnStatus = GetTurnStatus(player);
                action = player.Bot.PlayTurn(turnStatus);
            }
        }

        private TurnStatus GetTurnStatus(Player player)
        {
            return new TurnStatus
            {
                PlayerHand = new BlackJackHandDto(player.Hand),
                DealerHand = new BlackJackHandDto(Dealer.Hand)
            };
        }

        private void TabulateRoundResults()
        {
            foreach (var player in _players.Values)
            {
                var results = GetResultsForRound(player);

                _roundResults.Add(results);
                player.Bot.RoundComplete(results);
            }
        }

        private RoundResult GetResultsForRound(Player player)
        {
            var results = new RoundResult
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                DealerHand = new BlackJackHandDto(Dealer.Hand),
                PlayerHand = new BlackJackHandDto(player.Hand)
            };

            var playerTotal = player.Hand.GetSumOfHand();
            var dealerTotal = Dealer.Hand.GetSumOfHand();

            if (playerTotal > 21)
            {
                results.Result = PlayerResult.Bust;
                player.AddLoss();
            }
            else if (playerTotal == 21 && results.PlayerHand.Cards.Count() == 2)
            {
                results.Result = PlayerResult.Win;
                player.AddWin();
            }
            else if (playerTotal == dealerTotal)
            {
                results.Result = PlayerResult.Push;
                player.AddPush();
            }
            else if (playerTotal > dealerTotal || dealerTotal > 21)
            {
                results.Result = PlayerResult.Win;
                player.AddWin();
            }
            else
            {
                results.Result = PlayerResult.Loss;
                player.AddLoss();
            }
            return results;
        }

        public List<LeaderBoardEntry> GetLeaderBoard()
        {
            var leaderBoard = new Dictionary<Guid, LeaderBoardEntry>();

            foreach (var roundResult in _roundResults)
            {
                if (!leaderBoard.ContainsKey(roundResult.PlayerId))
                {
                    leaderBoard.Add(roundResult.PlayerId, new LeaderBoardEntry
                    {
                        PlayerId = roundResult.PlayerId,
                        PlayerName = roundResult.PlayerName
                    });

                }

                var leaderBoardEntry = leaderBoard[roundResult.PlayerId];

                switch (roundResult.Result)
                {
                    case PlayerResult.Bust:
                    case PlayerResult.Loss:
                        leaderBoardEntry.Losses++;
                        break;
                    case PlayerResult.Push:
                        leaderBoardEntry.Pushes++;
                        break;
                    case PlayerResult.Win:
                        leaderBoardEntry.Wins++;
                        break;
                }
                
            }

            return leaderBoard.Values.ToList();
        }
    }
}