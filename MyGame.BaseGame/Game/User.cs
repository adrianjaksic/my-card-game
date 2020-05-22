using System.Collections.Generic;

namespace MyGame.BaseGame.Game
{
    public class User
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public Stack<Card> DrawPile { get; private set; } = new Stack<Card>();

        public Stack<Card> DiscardPile { get; private set; } = new Stack<Card>();

        public Card Played { get; set; }

        public int CardCount
        {
            get
            {
                return DrawPile.Count + DiscardPile.Count + (Played != null ? 1 : 0);
            }
        }
    }
}
