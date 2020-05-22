using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame.BaseGame.Models;
using MyGame.Game40Implementation.Realization;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Tests
{
    [TestClass]
    public class Game40Tests
    {
        [TestMethod]
        public void TestNewGame()
        {
            var game = new Game40();
            Assert.AreEqual(40, game.Deck.Count);

            game.NewGame(new List<string> { "Player 1", "Player 2" });

            Assert.AreEqual(2, game.Users.Count);
            foreach (var user in game.Users)
            {
                Assert.AreEqual(20, user.DrawPile.Count);
            }            
        }

        [TestMethod]
        public void TestFirstRound()
        {
            var game = new Game40();
            game.NewGame(new List<string> { "Player 1", "Player 2" });
            game.DoRound();

            Assert.AreEqual(2, game.Users.Count);
            foreach (var user in game.Users)
            {
                Assert.AreEqual(19, user.DrawPile.Count);
                Assert.IsNotNull(user.Played);
                Assert.AreEqual(0, user.DiscardPile.Count);
            }
        }

        [TestMethod]
        public void NoDrawPileUseDiscardPile()
        {
            var game = new Game40();
            game.NewGame(new List<string> { "Player 1", "Player 2" });
            foreach (var user in game.Users)
            {
                user.DiscardPile = user.DrawPile.ToList();
                user.DrawPile.Clear();
            }

            game.DoRound();

            foreach (var user in game.Users)
            {
                Assert.AreEqual(19, user.DrawPile.Count);
                Assert.IsNotNull(user.Played);
                Assert.AreEqual(0, user.DiscardPile.Count);
            }
        }

        [TestMethod]
        public void HigherCardWins()
        {
            var game = new Game40();
            game.NewGame(new List<string> { "Player 1", "Player 2" });

            game.Users[0].DrawPile.Push(new Card() { Value = 1 });
            game.Users[1].DrawPile.Push(new Card() { Value = 2 });

            game.DoRound();
            Assert.AreEqual(game.Users[1], game.RoundWinner);
        }

        [TestMethod]
        public void NextRoundWinner()
        {
            var game = new Game40();
            game.NewGame(new List<string> { "Player 1", "Player 2" });

            game.Users[0].DrawPile.Push(new Card() { Value = 1 });
            game.Users[1].DrawPile.Push(new Card() { Value = 2 });
            game.Users[0].DrawPile.Push(new Card() { Value = 5 });
            game.Users[1].DrawPile.Push(new Card() { Value = 5 });
            
            game.DoRound();
            Assert.IsNull(game.RoundWinner);

            game.DoRound();
            Assert.AreEqual(game.Users[1], game.RoundWinner);
        }
    }
}
