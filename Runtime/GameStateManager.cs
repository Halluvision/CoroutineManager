using System;

namespace Halluvision.Coroutine
{
    public class GameStateManager
    {
        private static GameStateManager _instacne;
        public static GameStateManager Instance { get { if (_instacne == null) _instacne = new GameStateManager(); return _instacne; } }

        public Action<GameState> OnGameStateChanged;

        public GameState CurrentGameState { get; private set; }

        private GameStateManager() { }

        public void SetGameState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;

            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }
    }

    public enum GameState
    {
        Gameplay,
        Pause
    }
}