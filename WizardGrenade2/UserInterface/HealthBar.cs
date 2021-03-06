﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace WizardGrenade2
{
    public class HealthBar : Sprite
    {
        public bool IsTeamOut { get; private set; }

        private SpriteFont _spriteFont;
        private Color _teamTextColour;
        private Vector2 _teamNameOffset;
        private Vector2 _healthBarPosition;
        private readonly string _teamName;
        private readonly string _fileName;
        private int _startTeamHealth;
        private int _displayedTeamHealth;
        private int _framesH = 1;
        private int _framesV = 7;

        private Dictionary<string, int[]> _animationState = new Dictionary<string, int[]>
        {
            ["bar"] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5 }
        };

        public HealthBar(int teamNumber, int startTeamHealth, string teamName)
        {
            _fileName = @"UserInterface/HealthBar" + teamNumber;
            _teamName = teamName;
            _startTeamHealth = startTeamHealth;
            _displayedTeamHealth = _startTeamHealth;
            _teamNameOffset = new Vector2(-45, -2);
            _healthBarPosition = new Vector2(ScreenSettings.CentreScreenWidth, ScreenSettings.TARGET_HEIGHT -30 - (15 * teamNumber));
        }

        public void LoadContent(ContentManager contentManager)
        {
            LoadContent(contentManager, _fileName, _framesH, _framesV);
            LoadAnimationContent(_animationState);
            _spriteFont = contentManager.Load<SpriteFont>(@"Fonts/WizardHealthFont");
            SpriteScale = 1.2f;
            _healthBarPosition -= Origin;
        }

        public void Update(GameTime gameTime, int actualTeamHealth)
        {
            UpdateAnimationSequence("bar", 10, gameTime);
            SmoothUpdateHealthBar(gameTime, actualTeamHealth);
            float healthPercentage = 1 - ((float)_displayedTeamHealth / (float)_startTeamHealth);
            MaskSpriteRectangleWidth(1 - healthPercentage);
        }

        private void SmoothUpdateHealthBar(GameTime gameTime, int actualTeamHealth)
        {
            if (_displayedTeamHealth > actualTeamHealth)
                _displayedTeamHealth -= (int)(gameTime.ElapsedGameTime.TotalSeconds * 100);

            else if (actualTeamHealth <= 0)
                IsTeamOut = true;

            else
                _displayedTeamHealth = actualTeamHealth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _teamTextColour = IsTeamOut ? Color.Red : Color.White;

            DrawSprite(spriteBatch, _healthBarPosition);
            spriteBatch.DrawString(_spriteFont, _teamName, _healthBarPosition + _teamNameOffset, _teamTextColour);
        }
    }
}
