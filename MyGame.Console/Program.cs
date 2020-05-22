using MyGame.Game40Cards;
using MyGame.TextGame;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Console
{
    class Program
    {
        private static TextGameManager _gameManager;

        static void Main(string[] args)
        {
            List<string> users = CheckArguments(args);

            InitializeGame(users);

            char key;
            do
            {
                _gameManager.NewGame();
                System.Console.Write("Press q to end program, any key for new game...");
                key = System.Console.ReadKey().KeyChar;
            } while (key != 'q');
        }

        private static List<string> CheckArguments(string[] args)
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

            return users;
        }

        private static void InitializeGame(List<string> users)
        {
            _gameManager = new TextGameManager(new Game40(users));
        }
    }
}
