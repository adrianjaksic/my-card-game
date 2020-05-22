using MyGame.Game40Implementation.Realization;
using System;
using System.Collections.Generic;

namespace MyGame.Game40Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game40();
            game.NewGame(new List<string> { "Player 1", "Player 2" });
            while (game.GetWinner() == null)
            {
                game.DoRound();
                Console.WriteLine(game.GetRoundText());
                game.ClearRound();                
            }
            var winner = game.GetWinner();
            Console.WriteLine($"{winner.Name} wins the game!");
            Console.WriteLine();
            Console.Write("Press eny key to end game...");
            Console.ReadKey();
        }
    }
}
