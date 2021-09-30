using System;
using SFML.Graphics;
using SFML.Window;

namespace Pacman
{
    using static SFML.Window.Keyboard.Key;
    public class Pacman : Actor
    {
        private bool firstFrame;
        private float animationTimer;
        public override void Create(Scene scene)
        {
            speed = 100.0f;
            base.Create(scene);
            sprite.TextureRect = new IntRect(0, 0, 18, 18);
            scene.Events.LoseHealth += OnLoseHealth;
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            Position = originalPosition;
        }

        public override void Destroy(Scene scene)
        {
            scene.Events.LoseHealth -= OnLoseHealth;
        }

        protected override int PickDirection(Scene scene)
        {
            int dir = direction;

            if (Keyboard.IsKeyPressed(Right))
            {
                dir = 0; moving = true;

            }else if (Keyboard.IsKeyPressed(Up))
            {
                dir = 1; moving = true;

            }else if (Keyboard.IsKeyPressed(Left))
            {
                dir = 2; moving = true;

            }else if (Keyboard.IsKeyPressed(Down))
            {
                dir = 3; moving = true;
            }
            
            if (IsFree(scene, dir)) return dir;

            if (!IsFree(scene, direction)) moving = false;

            return direction;
        }

        private void Animation(float deltaTime)
        {
            animationTimer += deltaTime;
            if (animationTimer > 1 / 10f)
            {
                firstFrame = !firstFrame;
                animationTimer = 0;
            }

            switch (direction)
            {
                case 0: 
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 0, 18, 18);
                    break;
                case 1:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 18, 18, 18);
                    break;
                case 2:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 36, 18, 18);
                    break;
                case 3:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 54, 18, 18);
                    break;
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Animation(deltaTime);
            base.Update(scene, deltaTime);
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
        
    }
}
