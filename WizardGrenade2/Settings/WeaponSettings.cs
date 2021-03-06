﻿namespace WizardGrenade2
{
    public static class WeaponSettings
    {
        // General settings
        public static readonly int[] DETONATION_TIMES = new int[] { 1, 2, 3, 4, 5 };
        public const bool ROTATION = true;
        public const float MAX_DISTANCE = ScreenSettings.TARGET_WIDTH + 200;

        // Fireball settings
        private const string FIREBALL_FILENAME = @"GameObjects/Fireball";
        private const int FIREBALL_MASS = 35;
        private const int FIREBALL_COLLISION_POINTS = 12;
        private const float FIREBALL_BOUNCE_FACTOR = 0.5f;

        public const float FIREBALL_DETONATION_TIME = 4f;
        public const int FIREBALL_EXPLOSION_RADIUS = 40;
        public const int FIREBALL_CHARGE_POWER = 400;
        public const float FIREBALL_MAX_CHARGE_TIME = 2f;
        public const float FIREBALL_EXPLOSION_DAMPING = 0.6f;

        public static GameObjectParameters FIREBALL_GAMEOBJECT = new GameObjectParameters
            (FIREBALL_FILENAME, FIREBALL_MASS, ROTATION, FIREBALL_COLLISION_POINTS, FIREBALL_BOUNCE_FACTOR);

        // Arrow settings
        private const string ARROW_FILENAME = @"GameObjects/MelfsAcidArrow";
        private const int ARROW_MASS = 25;
        private const int ARROW_COLLISION_POINTS = 0;
        private const float ARROW_BOUNCE_FACTOR = 0f;

        public const int ARROW_POWER = 1500;
        public const float ARROW_MAX_CHARGE_TIME = 0.7f;
        public const float ARROW_KNOCKBACK_FACTOR = 1.4f;
        public const float ARROW_DAMAGE_FACTOR = 0.0245f;

        public static GameObjectParameters ARROW_GAMEOBJECT = new GameObjectParameters
            (ARROW_FILENAME, ARROW_MASS, ROTATION, ARROW_COLLISION_POINTS, ARROW_BOUNCE_FACTOR);

        // Icebomb Settings
        private const string ICEBOMB_FILENAME = @"GameObjects/IceBomb";
        private const int ICEBOMB_MASS = 70;
        private const int ICEBOMB_COLLISION_POINTS = 12;
        private const float ICEBOMB_BOUNCE_FACTOR = 0.1f;

        public const int ICEBOMB_CHARGE_POWER = 400;
        public const float ICEBOMB_MAX_CHARGE_TIME = 3f;
        public const int ICEBOMB_EXPLOSION_RADIUS = 60;
        public const int ICEBOMB_EFFECT_RADIUS = 100;
        public const float ICEBOMB_PUSHBACK_FACTOR = 1.2f;

        public static GameObjectParameters ICEBOMB_GAMEOBJECT = new GameObjectParameters
            (ICEBOMB_FILENAME, ICEBOMB_MASS, ROTATION, ICEBOMB_COLLISION_POINTS, ICEBOMB_BOUNCE_FACTOR);
    }
}
