using System.Collections.Generic;

namespace MyGame.BaseGame.Models
{
    public class User
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public Stack<Card> DrawPile { get; set; } = new Stack<Card>();

        public List<Card> DiscardPile { get; set; } = new List<Card>();

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
