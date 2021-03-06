﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WizardGrenade2
{
    public sealed partial class StateMachine
    {
        private StateMachine()
        {
            PlaceWizards();
        }
        private static readonly Lazy<StateMachine> lazyStateMachine = new Lazy<StateMachine>(() => new StateMachine());
        public static StateMachine Instance { get => lazyStateMachine.Value; }

        public GameStates GameState { get; private set; }
        private GameStates _previousGameState;
        private GameStates _currentGameState;
        private Timer _timer = new Timer(TIME_BETWEEN_TURNS);
        private ScreenText _screenText = new ScreenText();
        private const float TIME_BETWEEN_TURNS = 3f;

        public void LoadContent(ContentManager contentManager) => _screenText.LoadContent(contentManager);
        public void Draw(SpriteBatch spriteBatch) => _screenText.Draw(spriteBatch);
        public bool NewGameState() => _previousGameState != _currentGameState;
        public void ForceTurnEnd() => GameState = GameStates.BetweenTurns;
        public void ExitGame() => GameState = GameStates.ExitGame;
        public void RestartGame() => GameState = GameStates.PlaceWizards;
        public void ResetGame() => GameState = GameStates.Reset;

        public void UpdateStateMachine(GameTime gameTime)
        {
            _previousGameState = _currentGameState;
            _currentGameState = GameState;

            if (GameState == GameStates.BetweenTurns)
            {
                _timer.Update(gameTime);
                if (!_timer.IsRunning)
                {
                    GameState = GameStates.PlayerTurn;
                    _timer.ResetTimer(TIME_BETWEEN_TURNS);
                }
            }
        }

        public void PlaceWizards()
        {
            GameState = GameStates.PlaceWizards;
            _screenText.IsDisplaying = true;
        }

        public void WizardsPlaced()
        {
            if (GameState == GameStates.PlaceWizards)
            {
                GameState = GameStates.PlayerTurn;
                _screenText.IsDisplaying = false;
            }
        }

        public void ShotTaken()
        {
            if (GameState == GameStates.PlayerTurn)
                GameState = GameStates.ShotTaken;
        }

        public void ShotLanded()
        {
            if (GameState == GameStates.ShotTaken)
                GameState = GameStates.BetweenTurns;
        }

        public bool NewTurn()
        {
            return GameState == GameStates.PlayerTurn && 
                _previousGameState != _currentGameState && 
                _previousGameState != GameStates.PlaceWizards;
        }

        public void EndCurrentGame(string winningTeam)
        {
            GameState = GameStates.GameOver;

            if (NewGameState())
            {
                _screenText.IsDisplaying = true;
                _screenText.MainText = string.IsNullOrEmpty(winningTeam) ? "It's a Draw!" : winningTeam + " wins!";
                _screenText.InfoText = "Press 'Enter' to restart, or 'Delete' to quit";

                string gameEndSound = string.IsNullOrEmpty(winningTeam) ? "Draw" : "Win";
                SoundManager.Instance.PlaySoundInstance(gameEndSound);
            }

            if (InputManager.WasKeyPressed(Keys.Enter))
                ResetGame();
        }
    }
}
