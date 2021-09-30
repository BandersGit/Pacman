using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman
{
    public class GUI : Entity
    {
        private Text scoreText;
        private int maxHealth = 4;
        private int currentHealth;
        private int currentScore;

        public GUI() : base("pacman")
        {
            scoreText = new Text();
        }

        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(72, 36, 18, 18);
            scoreText.Font = scene.Assets.LoadFont("pixel-font");
            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.FillColor = Color.Black;
            currentHealth = maxHealth;
            base.Create(scene);

            scene.LoseHealth += OnLoseHealth;
            scene.GainScore += OnGainScore;
        }

        private void OnGainScore(Scene scene, int amount)
        {
            currentScore += amount;
            scoreText.DisplayedString = $"Score: {currentScore}";
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                scene.Loader.Reload();
            }
        }

        public override void Render(RenderTarget target)
        {
            sprite.Position = new Vector2f(36, 396);
            scoreText.CharacterSize = 36;
            scoreText.Scale = new Vector2f(0.5f, 0.5f);

            for (int i = 0; i < maxHealth; i++)
            {
                sprite.TextureRect = i < currentHealth 
                    ? new IntRect(72, 36, 18, 18) 
                    : new IntRect(72, 0, 18, 18);
                base.Render(target);
                sprite.Position += new Vector2f(18, 0);
            }
            
            scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
            target.Draw(scoreText);
        }
    }
}
