using MyGame.Game40Implementation.Realization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Game40Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> users;
            if (args.Length == 2)
            {
                users = args.ToList();
            }
            else
            {
                users = new List<string> { "Player 1", "Player 2" };
            }

            var game = new Game40(users);
            char key;
            do
            {
                NewGame(game);
                Console.Write("Press q to end program, any key for new game...");
                key = Console.ReadKey().KeyChar;
            } while (key != 'q');
        }

        private static void NewGame(Game40 game)
        {
            game.NewGame();
            while (game.GetWinner() == null)
            {
                game.DoRound();
                Console.WriteLine(game.GetRoundText());
                game.ClearRound();
            }
            var winner = game.GetWinner();
            Console.WriteLine($"{winner.Name} wins the game!");
            Console.WriteLine();
            
        }
    }
}
