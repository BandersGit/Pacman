using System;

namespace Pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);

    public class EventManager //Creates and handles what happens with the events and what subscribers can see and use
    {
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent EatCandy;

        private int scoreGained;
        private int healthLost;
        private int candiesEaten;

        public void PublishGainScore(int amount) => scoreGained += amount;
        public void PublishLoseHealth(int amount) => healthLost += amount;
        public void PublishEatCandy(int amount) => candiesEaten += amount;

        public void HandleEvents(Scene scene)
        {
            if (healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }

            if (candiesEaten != 0)
            {
                EatCandy?.Invoke(scene, candiesEaten);
                candiesEaten = 0;
            }
        }

        public void LateHandleEvents(Scene scene) //To make the game reset when the last coin is picked up it needs to check it after the "Dead" check
        {
            if (scoreGained != 0)
            {
                GainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
        }
    }
}