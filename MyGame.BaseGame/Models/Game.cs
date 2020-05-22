using System.Collections.Generic;
using System.Text;

namespace MyGame.BaseGame.Models
{
    public abstract class Game
    {
        public Game(string gameName)
        {
            GameName = gameName;
        }

        public string GameName { get; private set; }

        public Stack<Card> Deck { get; protected set; }

        public Stack<Card> PlayedDeck { get; protected set; }

        public List<User> Users { get; protected set; }

        public User RoundWinner { get; protected set; }

        public abstract void NewGame(List<string> names);

        public abstract void DoRound();

        public virtual string GetRoundText()
        {
            var sb = new StringBuilder();
            foreach (var user in Users)
            {
                sb.AppendLine($"{user.Name} ({user.CardCount} cards): {user.Played}");
            }
            //if (PlayedDeck.Count > 0)
            //{
            //    sb.AppendLine($"Cards from previous round: {PlayedDeck.Count}");
            //}
            if (RoundWinner != null)
            {
                sb.AppendLine($"{RoundWinner.Name} wins this round");
            }
            else
            {
                sb.AppendLine("No winner in this round");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public abstract void ClearRound();

        public abstract User GetWinner();
    }
}
