namespace MyGame.BaseGame.Game
{
    public abstract class GameManager
    {
        protected readonly Game _game;
        public GameManager(Game game)
        {
            _game = game;
        }

        public void NewGame()
        {
            _game.NewGame();
            while (_game.GetWinner() == null)
            {
                _game.DoRound();
                DrawRound();
                _game.ClearRound();
            }
            var winner = _game.GetWinner();
            DrawEndGame(winner);
        }

        protected abstract void DrawRound();

        protected abstract void DrawEndGame(User winner);
    }
}
