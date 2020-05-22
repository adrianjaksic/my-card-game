using MyGame.BaseGame.Models;
using MyGame.BaseGame.Shuffle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Game40Implementation.Realization
{
    public class Game40 : Game
    {
        public Game40(IShuffler<Card> shuffler) : base("Game40")
        {
            _shuffler = shuffler;
            CreateDeck();
        }

        public Game40() : this(new FisherYatesShuffler<Card>()) { }

        private IShuffler<Card> _shuffler;

        private const int NumberOfCardsInDeck = 40;

        private void CreateDeck()
        {
            var deck = new List<Card>();
            for (var i = 1; i <= NumberOfCardsInDeck / 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    deck.Add(new Card()
                    {
                        Value = i
                    });
                }
            }
            Deck = new Stack<Card>(deck);
            PlayedDeck = new Stack<Card>();
        }

        #region NewGame
        public override void NewGame(List<string> names)
        {
            if (names == null)
            {
                throw new ArgumentNullException("name");
            }
            if (names.Count != 2)
            {
                throw new ArgumentException("name");
            }

            SetUsers(names);
            SuffleDeck();
            DistributeDecksPerUsers();
        }

        private void SetUsers(List<string> names)
        {
            Users = new List<User>();
            foreach (var name in names)
            {
                Users.Add(new User(name));
            }
            RoundWinner = null;
        }

        private void SuffleDeck()
        {
            Deck = new Stack<Card>(_shuffler.Shuffle(Deck.ToList()));
        }

        private void DistributeDecksPerUsers()
        {
            var usersQueue = new Queue<User>(Users);
            while (Deck.Any())
            {
                var user = usersQueue.Dequeue();
                user.DrawPile.Push(Deck.Pop());
                usersQueue.Enqueue(user);
            }
        }

        #endregion

        #region DoRound

        public override void DoRound()
        {
            UseDiscardPile();
            PlayCard();
            SetRoundWinner();
        }

        private void UseDiscardPile()
        {
            var usersWithoutCard = Users.Where(u => !u.DrawPile.Any()).ToList();
            foreach (var user in usersWithoutCard)
            {
                user.DrawPile = new Stack<Card>(new FisherYatesShuffler<Card>().Shuffle(user.DiscardPile));
                user.DiscardPile.Clear();
            }
        }

        private void PlayCard()
        {
            foreach (var user in Users)
            {
                if (user.DrawPile.Any())
                {
                    user.Played = user.DrawPile.Pop();
                }
            }
        }

        private void SetRoundWinner()
        {
            Card biggestCard = null;
            List<User> ussersWithBiggestCard = new List<User>();
            foreach (var user in Users.Where(u => u.Played != null))
            {
                if (biggestCard == null || user.Played.Value > biggestCard.Value)
                {
                    biggestCard = user.Played;
                    ussersWithBiggestCard.Clear();
                }
                if (user.Played.Value == biggestCard.Value)
                {
                    ussersWithBiggestCard.Add(user);
                }
            }
            if (ussersWithBiggestCard.Count == 1)
            {
                RoundWinner = ussersWithBiggestCard.First();
            }
        }

        #endregion

        #region ClearRound

        public override void ClearRound()
        {
            AddCardsToWinner();
            foreach (var user in Users)
            {
                if (user.Played != null)
                {
                    PlayedDeck.Push(user.Played);
                    user.Played = null;
                }
            }
            base.RoundWinner = null;
        }

        private void AddCardsToWinner()
        {
            if (RoundWinner != null)
            {
                if (PlayedDeck.Any())
                {
                    RoundWinner.DiscardPile.AddRange(PlayedDeck);
                    PlayedDeck.Clear();
                }
                foreach (var user in Users)
                {
                    if (user.Played != null)
                    {
                        RoundWinner.DiscardPile.Add(user.Played);
                        user.Played = null;
                    }
                }
            }
        }


        #endregion

        #region CheckWinner

        public override User GetWinner()
        {
            foreach (var user in Users)
            {
                if (user.CardCount == NumberOfCardsInDeck)
                {
                    return user;
                }
            }
            return null;
        }

        #endregion
    }
}
