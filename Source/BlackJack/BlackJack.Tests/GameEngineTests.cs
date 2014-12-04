using System;
using System.Linq;
using System.Runtime;
using BlackJack.Core;
using BlackJack.Core.Bots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackJack.Tests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void AddPlayerToGame_AddsThePlayer()
        {
            var engine = new GameEngine();
            engine.AddPlayerToGame(new PeterGriffinBot());

            Assert.IsTrue(engine.PlayersWithDealer.Count() == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void RunGame_WontStartWithoutPlayersAssigned()
        {
            var engine = new GameEngine();
            engine.RunGame();
        }

        [TestMethod]
        public void DealCards_DealsCardsToAllPlayers()
        {
            var engine = new GameEngine();
            engine.AddPlayerToGame(new PeterGriffinBot());
            
            engine.DealCards();

            Assert.IsTrue(engine.PlayersWithDealer.Count(player => player.Hand.NumCards < 2) == 0);
            Assert.AreEqual(engine.Dealer.Hand.NumCards, 2);
        }

        [TestMethod]
        public void RunGame_DealsCardsAndPlaysRounds()
        {
            var engine = new GameEngine();
            engine.AddPlayerToGame(new PeterGriffinBot());
            engine.AddPlayerToGame(new CommonSenseBot());
            engine.AddPlayerToGame(new JavaScriptBot());
            //engine.AddPlayerToGame(new CommonSenseBot());
            //engine.AddPlayerToGame(new JavaScriptBot());
            //engine.AddPlayerToGame(new CommonSenseBot());
            //engine.AddPlayerToGame(new JavaScriptBot());
            //engine.AddPlayerToGame(new CommonSenseBot());

            engine.RunGame();

        }

        //[TestMethod]
        //public void RunGame_CanRunJavaScriptBot()
        //{
        //    var engine = new GameEngine();
        //    engine.AddPlayerToGame(new JavaScriptBot());
        //    engine.RunGame();
        //}
    }
}
