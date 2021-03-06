﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace WizardGrenade2
{
    public class Potion
    {
        private GameObject _potion;
        private GameObjectParameters _potionParameters;
        private Random _random = new Random();
        private readonly string _fileName = @"GameObjects/Potions";
        private const int MASS = 75;
        private const float BOUNCE_FACTOR = 0.2f;

        private Dictionary<string, int[]> _potionTypes = new Dictionary<string, int[]>()
        {
            ["0"] = new int[] { 0 },
            ["1"] = new int[] { 1 },
            ["2"] = new int[] { 2 },
        };

        public Potion()
        {
            _potionParameters = new GameObjectParameters(_fileName, MASS, false, 0, BOUNCE_FACTOR, 3, 1);
            _potion = new GameObject(_potionParameters);
        }

        public void LoadContent(ContentManager contentManager)
        {
            int type = _random.Next(0, 2);
            _potion.LoadContent(contentManager);
            _potion.LoadAnimationContent(_potionTypes);
            _potion.UpdateAnimationFrame(type.ToString(), type);
            _potion.Position = new Vector2(ScreenSettings.CentreScreenWidth, 0);
        }

        public void Update(GameTime gameTime)
        {
            _potion.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            _potion.Draw(spriteBatch);
        }
    }
}
