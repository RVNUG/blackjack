using EdgeJs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core.Bots
{
    public class JavaScriptBot : IPlayerBot
    {
        private Func<object, Task<object>> _bot;
        private dynamic _botState = new { };

        public JavaScriptBot(string botJavaScriptSource = null)
        {
            if (botJavaScriptSource == null)
            {
                botJavaScriptSource = Properties.Resources.SampleJsBot;
            }
            
            botJavaScriptSource = botJavaScriptSource + @"
return function (data, callback) {
    var bot = new BlackJackBot();
    var args = data.args;
    args.unshift(data.state);

    var botFunc = bot[data.method] || function () { };

    var returnValue = botFunc.apply(bot, args);

    callback(null, {
        returnValue: returnValue,
        state: data.state
    });
};";            

            _bot = Edge.Func(botJavaScriptSource);
        }

        public string RegisterBot(int playerPosition)
        {
            var task = _bot(new
            {
                method = "registerBot",
                args = new object[] { playerPosition },
                state = _botState
            });
            task.Wait();

            dynamic results = task.Result;
            var botName = (string)results.returnValue;
            _botState = results.state;

            return botName;
        }

        public void CardDealt(int playerPosition, CardDto card)
        {
            var task = _bot(new
            {
                method = "cardDealt",
                args = new object[] { playerPosition, card },
                state = _botState
            });
            task.Wait();

            dynamic results = task.Result;
            _botState = results.state;
        }

        public PlayerAction PlayTurn(TurnStatus status)
        {
            var task = _bot(new
            {
                method = "playTurn",
                args = new object[] { status },
                state = _botState
            });

            task.Wait();

            PlayerAction action;

            dynamic results = task.Result;
            Enum.TryParse(results.returnValue.ToString(), true, out action);
            return action;
        }

        public void RoundComplete(RoundResult result)
        {
            var task = _bot(new
            {
                method = "roundComplete",
                args = new object[] { result },
                state = _botState
            });

            task.Wait();
        }

        public void CardsShuffled()
        {
            var task = _bot(new
            {
                method="cardsShuffled",
                args = new object[] { },
                state = _botState
            });

            task.Wait();
        }

        public void UpdateBotCode(string botJavaScriptSource)
        {
            botJavaScriptSource = botJavaScriptSource + @"
return function (data, callback) {
    var bot = new BlackJackBot();
    var args = data.args;
    args.unshift(data.state);

    var botFunc = bot[data.method] || function () { };

    var returnValue = botFunc.apply(bot, args);

    callback(null, {
        returnValue: returnValue,
        state: data.state
    });
};";
            _bot = Edge.Func(botJavaScriptSource);
        }

        public static string SampleJavaScriptBotCode = Properties.Resources.SampleJsBot;
    }
}