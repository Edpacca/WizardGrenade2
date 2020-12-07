﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace WizardGrenade2
{
    public sealed class WeaponManager
    {
        private WeaponManager() {}
        private static readonly Lazy<WeaponManager> lazyManager = new Lazy<WeaponManager>(() => new WeaponManager());
        public static WeaponManager Instance { get => lazyManager.Value; }

        private Fireball _fireball = new Fireball();
        private Arrow _arrow = new Arrow();
        private IceBomb _iceBomb = new IceBomb();
        private Crosshair _crosshair = new Crosshair();
        private List<Wizard> _allWizards;

        private HugeFireball _hugeFireball = new HugeFireball();

        private int _numberOfWeapons;
        private int _timer = 4;
        private bool _isLoaded;

        public float ChargePower { get; private set; }
        public int ActiveWeapon { get; private set; }
        public bool IsCharging { get; private set; }
        public List<Weapon> Weapons { get; private set; }
        public Rectangle WizardSpriteRectangle { get; private set; }
        private Vector2 _initialPosition;
        public Vector2 ActiveWizardPosition;

        private Random _random = new Random();

        private readonly int[] _detonationTimes = new int[] { 1, 2, 3, 4, 5 };
        
        public void LoadContent(ContentManager contentManager, List<Wizard> allWizards)
        {
            PopulateGameObjects(allWizards);

            Weapons = new List<Weapon>();
            Weapons.Add(_fireball);
            Weapons.Add(_arrow);
            Weapons.Add(_iceBomb);

            foreach (var weapon in Weapons)
                weapon.LoadContent(contentManager);

            _hugeFireball.LoadContent(contentManager);

            _numberOfWeapons = Weapons.Count;
            _crosshair.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime, Vector2 activeWizardPosition, int activeDirection)
        {
            ActiveWizardPosition = activeWizardPosition;
            _crosshair.Update(gameTime, activeWizardPosition, activeDirection, ChargePower, Weapons[ActiveWeapon].MaxChargeTime);
            CycleWeapons(Keys.Tab);

            if (_isLoaded)
                ChargeWeapon(gameTime, activeWizardPosition, activeDirection);

            ResetCharge();

            Weapons[ActiveWeapon].Update(gameTime, _allWizards);
            UpdateGrenadeTimer();
            ResetTimer();

            if (!_isLoaded && Vector2.Distance(_initialPosition, Weapons[ActiveWeapon].Position) > ScreenSettings.TARGET_WIDTH + 200)
                Weapons[ActiveWeapon].KillProjectile();

            _hugeFireball.Update(gameTime, _allWizards);
        }

        public void PopulateGameObjects(List<Wizard> allWizards)
        {
            _allWizards = allWizards;
            WizardSpriteRectangle = _allWizards[0].GetSpriteRectangle();
        }

        private void CycleWeapons(Keys key)
        {
            if (InputManager.WasKeyPressed(key) && _isLoaded)
            {
                int randomSound = _random.Next(1, 5);
                SoundManager.Instance.PlaySound("magic" + randomSound);
                Weapons[ActiveWeapon].IsMoving = false;
                ActiveWeapon = Utility.WrapAroundCounter(ActiveWeapon, _numberOfWeapons);
                Weapons[ActiveWeapon].IsActive = true;
            }
        }

        private void ChargeWeapon(GameTime gameTime, Vector2 activePlayerPosition, int activeDirection)
        {
            if (InputManager.IsKeyDown(Keys.Space) && 
                StateMachine.Instance.GameState == StateMachine.GameStates.PlayerTurn)
            {
                SoundManager.Instance.PlaySoundInstance(Weapons[ActiveWeapon].ChargingSoundFile);
                IsCharging = true;
                Weapons[ActiveWeapon].KillProjectile();
                Weapons[ActiveWeapon].SetToPlayerPosition(activePlayerPosition, activeDirection, _crosshair.CrosshairAngle);
                ChargePower += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (InputManager.WasKeyReleased(Keys.Space) || ChargePower >= Weapons[ActiveWeapon].MaxChargeTime)
            {
                SoundManager.Instance.StopSoundInstance(Weapons[ActiveWeapon].ChargingSoundFile);
                SoundManager.Instance.PlaySoundInstance(Weapons[ActiveWeapon].MovingSoundFile);
                _isLoaded = false;
                IsCharging = false;
                Weapons[ActiveWeapon].FireProjectile(ChargePower, _crosshair.CrosshairAngle);
                _initialPosition = activePlayerPosition;
                ChargePower = 0f;
                StateMachine.Instance.ShotTaken();
            }
        }

        private void ResetCharge()
        {
            if (!Weapons[ActiveWeapon].IsMoving && StateMachine.Instance.GameState == StateMachine.GameStates.PlayerTurn)
                _isLoaded = true;
        }

        private void UpdateGrenadeTimer()
        {
            if (_isLoaded)
            {
                int numberKey = InputManager.NumberKeys();

                foreach (var time in _detonationTimes)
                {
                    if (numberKey == time)
                    {
                        SetTimer(numberKey);
                        _timer = numberKey;
                    }
                }
            }
        }

        private bool IsTimerNull() => (Weapons[ActiveWeapon].DetonationTimer == null);
        public float GetDetonationTime() => IsTimerNull() ? 0 : Weapons[ActiveWeapon].DetonationTimer.Time;

        private void SetTimer(int time)
        {
            if (!IsTimerNull())
                Weapons[ActiveWeapon].DetonationTimer.ResetTimer(time);
        }

        private void ResetTimer()
        {
            if (!IsTimerNull() && StateMachine.Instance.NewTurn())
                Weapons[ActiveWeapon].DetonationTimer.ResetTimer(_timer);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Weapons[ActiveWeapon].Draw(spriteBatch);

            if (StateMachine.Instance.GameState == StateMachine.GameStates.PlayerTurn)
            _crosshair.Draw(spriteBatch, IsCharging);

            _hugeFireball.Draw(spriteBatch);
        }

        public void SpawnHugeFireballs()
        {
            _hugeFireball.SpawnFireballs();
        }
    }
}