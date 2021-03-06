﻿using Microsoft.Xna.Framework;
using System;

namespace WizardGrenade2
{
    public static class Utility
    {
        public static float CalcMinTheta(float radius, float minLength) => 2 * (float)Math.Asin(minLength / (2 * radius));
        public static float FlipAngle(float initialAngle) => (float)(Math.PI + (Math.PI - initialAngle));
        public static float FractionPercentage(float fraction, float percentage, int sign) => fraction + (sign * fraction * percentage);
        public static float DifferentialGameTimeValue(GameTime gameTime, int rateFactor, int magnitude) => (float)gameTime.ElapsedGameTime.TotalSeconds * rateFactor * magnitude;
        public static int WrapAroundCounter(int number, int listLength) => (number + 1) % listLength;
        public static int WrapAroundNegativeCounter(int number, int listLength) => number - 1 < 0 ? listLength - number - 1 : number - 1;
        public static int ChangeValueInLimits(int nextValue, int minValue, int maxValue) => nextValue < minValue ? minValue : nextValue > maxValue ? maxValue : nextValue;
        public static bool Approximate(float float1, float float2) => Math.Abs(float1 - float2) < 0.00001f;
        public static bool isWithinCircleInSquare(int radius, int x, int y) => Math.Pow((x - radius), 2) + Math.Pow((y - radius), 2) <= Math.Pow(radius, 2);
        public static bool Approx(float f1, float f2) => (Math.Abs(f1 - f2) < (Math.Abs(f1) * 1e-9));

        public static Rectangle ShiftRectangle(Rectangle rectangle, Vector2 offset, float scaleFactor)
        {
            return new Rectangle((int)((rectangle.X + offset.X) / scaleFactor), (int)((rectangle.Y + offset.Y) / scaleFactor),
                                (int)(rectangle.Width / scaleFactor), (int)(rectangle.Height / scaleFactor));
        }

        public static bool isPointWithinCircle(Vector2 testPosition, Vector2 circleCentre, float circleRadius)
        {
            Vector2 deltaRadius = testPosition - circleCentre;
            float deltaMagnitude = (float)Math.Pow(Mechanics.VectorMagnitude(deltaRadius), 2);
            return deltaMagnitude <= Math.Pow(circleRadius, 2);
        }
    }
}
