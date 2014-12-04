using BlackJack.Core;
using BlackJack.Core.Bots;
using BlackJack_RVNUG.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BlackJack.Controllers
{
    public class GameController : ApiController
    {
        //private const string PlayerIdCookieName = "playerId";
        private const string JsBotPlayerNameSearchString = "{{BOTNAME}}";
        private static GameEngine _gameEngine = new GameEngine(
        new IPlayerBot[]{
            new PeterGriffinBot(),
            new CommonSenseBot()
        });

        private static int _uniquePlayerId = 1;

        public GameController()
        {
            //_gameEngine.AddPlayerToGame(new JavaScriptBot());
        }

        // GET api/Game
        [HttpGet]
        [Route("api/game")]
        public IHttpActionResult GetGameResults()
        {
            _gameEngine.RunGame();

            return Ok(_gameEngine.GetLeaderBoard().ToArray());
        }

        //[HttpPost]
        //[Route("api/player")]
        //public HttpResponseMessage AddPlayer([FromBody]string botJavaScriptCode)
        //{
        //    var player = (String.IsNullOrWhiteSpace(botJavaScriptCode)) ?
        //        new JavaScriptBot() :
        //        new JavaScriptBot(botJavaScriptCode);

        //    var playerId = _gameEngine.AddPlayerToGame(player);
        //    var resp = new HttpResponseMessage();

        //    var cookie = new CookieHeaderValue(PlayerIdCookieName, playerId.ToString())
        //    {
        //        Expires = DateTimeOffset.Now.AddDays(1),
        //        Domain = Request.RequestUri.Host,
        //        Path = "/"
        //    };

        //    resp.Headers.AddCookies(new[] { cookie });

        //    resp.StatusCode = HttpStatusCode.OK;
        //    resp.Content = new StringContent(playerId.ToString());

        //    return resp;
        //}

        [HttpPut]
        [Route("api/player/{id}")]
        public IHttpActionResult UpdateBot(Guid id, [FromBody]BotDTO bot)
        {
            var player = _gameEngine.GetPlayer(bot.Id);

            if (player == null || !(player.Bot is JavaScriptBot))
            {
                return NotFound();
            }

            // Update the bot in the static game engine that is used to report the leaderboard
            (player.Bot as JavaScriptBot).UpdateBotCode(bot.JavaScriptCode);

            var name = player.Bot.RegisterBot(player.Position);
            player.Name = name;

            // now create a new single-use game engine where we can test this bot
            // against only the dealer and return those results
            var tempEng = new GameEngine();
            tempEng.AddPlayerToGame(new JavaScriptBot(bot.JavaScriptCode));

            tempEng.RunGame();
            return Ok(tempEng.RoundResults.ToArray());
        }

        [HttpGet]
        [Route("api/player")]
        public IHttpActionResult GetSampleBot()
        {
            var sampleBotCode = JavaScriptBot.SampleJavaScriptBotCode;
            var playerName = String.Format("Player {0}", _uniquePlayerId++);

            sampleBotCode = sampleBotCode.Replace(
                JsBotPlayerNameSearchString,
                playerName);

            var player = new JavaScriptBot(sampleBotCode);
            var playerId = _gameEngine.AddPlayerToGame(player);

            return Ok(new BotDTO { Id = playerId, JavaScriptCode = sampleBotCode });
        }
    }
}