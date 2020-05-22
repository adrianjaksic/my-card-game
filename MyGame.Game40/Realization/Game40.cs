using MyGame.BaseGame.Extensions;
using MyGame.BaseGame.Models;
using MyGame.BaseGame.Shuffle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Game40Implementation.Realization
{
    public class Game40 : Game
    {
        private const int NumberOfCardsInDeck = 40;

        #region Constructors

        public Game40(IShuffler<Card> shuffler, List<string> userNames) : base("Game40", shuffler)
        {
            CreateDeck();
            SetUsers(userNames);
        }

        public Game40(List<string> userNames) : this(new FisherYatesShuffler<Card>(), userNames) { }

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

        private void SetUsers(List<string> userNames)
        {
            if (userNames != null)
            {
                Users = new List<User>();
                foreach (var user in userNames)
                {
                    Users.Add(new User(user));
                }
            }
        }

        #endregion

        #region NewGame

        public override void NewGame()
        {
            if (Users == null || Users.Count != 2)
            {
                throw new ApplicationException("User number not right.");
            }
            RoundWinner = null;
            GetCardsBackToDeck();            
            ShuffleDeck();
            DistributeDecksPerUsers();
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
            PlayCard();
            SetRoundWinner();
        }

        private void PlayCard()
        {
            foreach (var user in Users)
            {
                DrawCardFromDeck(user);
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
            RoundWinner = null;
        }

        private void AddCardsToWinner()
        {
            if (RoundWinner != null)
            {
                if (PlayedDeck.Any())
                {
                    RoundWinner.DiscardPile.AddToStackAndEmptySource(PlayedDeck);
                }
                foreach (var user in Users)
                {
                    if (user.Played != null)
                    {
                        RoundWinner.DiscardPile.Push(user.Played);
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
