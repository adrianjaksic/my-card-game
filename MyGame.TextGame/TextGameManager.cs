using MyGame.BaseGame.Game;
using System;
using System.Text;

namespace MyGame.TextGame
{
    public class TextGameManager : GameManager
    {
        public TextGameManager(Game game) : base(game) { }

        protected override void DrawRound()
        {
            var sb = new StringBuilder();
            foreach (var user in _game.Users)
            {
                sb.AppendLine($"{user.Name} ({user.CardCount} cards): {user.Played}");
            }
            if (_game.PlayedDeck.Count > 0)
            {
                sb.AppendLine($"Cards in play from previous round: {_game.PlayedDeck.Count}");
            }
            if (_game.RoundWinner != null)
            {
                sb.AppendLine($"{_game.RoundWinner.Name} wins this round");
            }
            else
            {
                sb.AppendLine("No winner in this round");
            }
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }        

        protected override void DrawEndGame(User winner)
        {
            Console.WriteLine($"{winner.Name} wins the game!");
            Console.WriteLine();
        }
    }
}
