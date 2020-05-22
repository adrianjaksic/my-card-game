using MyGame.BaseGame.Extensions;
using MyGame.BaseGame.Shuffle;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.BaseGame.Game
{
    public abstract class Game
    {
        #region Private fields

        private IShuffler<Card> _shuffler;

        #endregion

        #region Constructor

        public Game(string gameName, IShuffler<Card> shuffler)
        {
            GameName = gameName;
            _shuffler = shuffler;
        }

        #endregion

        #region Game fields

        public string GameName { get; set; }

        public Stack<Card> Deck { get; set; }

        public Stack<Card> PlayedDeck { get; set; }

        public List<User> Users { get; set; }

        public User RoundWinner { get; set; }

        #endregion

        #region Game methods

        public abstract void NewGame();

        public abstract void DoRound();

        public abstract void ClearRound();

        public abstract User GetWinner();

        #endregion

        #region Internal methods

        protected void GetCardsBackToDeck()
        {
            if (Users != null)
            {
                foreach (var user in Users)
                {
                    Deck.AddToStackAndEmptySource(user.DrawPile);
                    Deck.AddToStackAndEmptySource(user.DiscardPile);
                }
                Deck.AddToStackAndEmptySource(PlayedDeck);
            }
        }

        protected void ShuffleDeck()
        {
            _shuffler.ShuffleStack(Deck);
        }

        protected void DrawCardFromDeck(User user)
        {
            if (user.DrawPile.Count == 0)
            {
                _shuffler.ShuffleStack(user.DiscardPile);
                user.DrawPile.AddToStackAndEmptySource(user.DiscardPile);
            }
            if (user.DrawPile.Any())
            {
                user.Played = user.DrawPile.Pop();
            }
        }

        #endregion
    }
}
