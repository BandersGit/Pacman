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
            scene.Events.LoseHealth += OnLoseHealth; //Runs the method when the subscribed event is activated
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            ResetActor();
        }

        public override void Destroy(Scene scene)
        {
            scene.Events.LoseHealth -= OnLoseHealth;
        }

        protected override int PickDirection(Scene scene) //Uses keyboard input to determine a int value that gets sent to
        {                                                 //A check if it can move int that direction and moves it if it is allowed
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

        private void Animation(float deltaTime) //Uses deltaTime to time the frame switches and uses the same int value that checks the movement
        {                                       //in order to know the orientation of the sprite that will be animated
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
