﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WizardGrenade2
{
    public class Scroll
    {
        public bool IsUnrolled { get; private set; }
        private Sprite _scrollTop;
        private Sprite _scrollBottom;
        private Vector2 _position;
        private Vector2 _topPosition;
        private Vector2 _bottomPosition;
        private float _yOffset;
        private float _positionOffset = 10f;
        private float _percentage;
        private const float ANIMATION_SPEED = 800;

        public Scroll(Vector2 position)
        {
            _position = position;
        }

        public void LoadContent(ContentManager contentManager)
        {
            _scrollTop = new Sprite(contentManager, @"Menu/Scroll0");
            _scrollBottom = new Sprite(contentManager, @"Menu/Scroll1");

            _topPosition = _position - _scrollTop.Origin;
            _bottomPosition = _topPosition;
            _yOffset = _scrollBottom.SpriteRectangle.Height;
            ResetPauseMenu();
        }

        public void ResetPauseMenu()
        {
            _bottomPosition.Y = _position.Y - _yOffset;
            _scrollTop.MaskSpriteRectangleHeight(_topPosition.Y - _bottomPosition.Y / _topPosition.Y);
        }

        public void Update(GameTime gameTime)
        {
            if (_bottomPosition.Y < _topPosition.Y - _positionOffset)
            {
                _bottomPosition.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * ANIMATION_SPEED;
                _percentage = 1 - ((_topPosition.Y - _bottomPosition.Y + 80) / _yOffset);
                _scrollTop.MaskSpriteRectangleHeight(_percentage);
                IsUnrolled = false;
            }
            else
            {
                _scrollTop.MaskSpriteRectangleHeight(1);
                IsUnrolled = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _scrollTop.DrawSprite(spriteBatch, _topPosition);
            _scrollBottom.DrawSprite(spriteBatch, _bottomPosition);
        }
    }
}
