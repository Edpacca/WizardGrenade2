﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WizardGrenade2
{
    class GameScreen
    {
        public int[] TeamHealths { get => _battleManager.TeamHealths; }
        public List<string> TeamNames { get => _battleManager.TeamNames; }
        private BattleManager _battleManager;
        private UserInterface _userInterface;

        public GameScreen(GameOptions gameOptions, ContentManager contentManager)
        {
            _userInterface = new UserInterface(gameOptions, contentManager);
            Initialise(gameOptions);
            LoadContent(contentManager);
        }

        public void Initialise(GameOptions gameOptions)
        {
            _battleManager = new BattleManager(gameOptions.MapFile);
            _battleManager.Initialise(gameOptions);
        }

        private void LoadContent(ContentManager contentManager)
        {
            _battleManager.LoadContent(contentManager);

        }

        public void Update(GameTime gameTime)
        {
            _userInterface.Update(gameTime, TeamHealths);
            _battleManager.Update(gameTime);
        }

        public void DrawGameLayer(SpriteBatch spriteBatch)
        {
            _battleManager.Draw(spriteBatch);
        }

        public void DrawUILayer(SpriteBatch spriteBatch)
        {
            _userInterface.Draw(spriteBatch);
        }
    }
}
