using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class Ghost : Actor
    {
        private float frozenTimer;
        private bool firstFrame;
        private float animationTimer;

        public override void Create(Scene scene)
        {
            direction = -1;
            speed = 100.0f;
            moving = true;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            scene.Events.EatCandy += OnCandyEaten; //Runs the method when the subscribed event is activated
        }

        public override void Destroy(Scene scene)
        {
            scene.Events.EatCandy -= OnCandyEaten;
        }

        private void OnCandyEaten(Scene scene, int amount)
        {
            frozenTimer = 5.0f;
        }

        protected override void CollideWith(Scene scene, Entity e) //If pacman collides with a ghost that is not "frozen" he will lose health
        {
            if (e is Pacman)
            {
                if (frozenTimer <= 0.0f)
                {
                    scene.Events.PublishLoseHealth(1);
                }else
                {
                    scene.Events.PublishGainScore(200);
                }
                
                ResetActor();
                
            }
        }

        protected override int PickDirection(Scene scene)
        {
            List<int> validMoves = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if ((i + 2) % 4 == direction) continue;

                if (IsFree(scene, i))
                {
                    validMoves.Add(i);
                }
            }
            
            int r = new Random().Next(0, validMoves.Count);
            return validMoves[r];
        }

        private void Animation(float deltaTime) //Similar to Platformer, uses deltaTime to switch between animation frames
        {                                       //Would like to call the method in Render but then there is no acess to deltaTime
            animationTimer += deltaTime;
            if (animationTimer > 1 / 5f)
            {
                firstFrame = !firstFrame;
                animationTimer = 0;
            }

            if (frozenTimer > 0.0f)
            {
                if (firstFrame)
                {
                    sprite.TextureRect = new IntRect(36, 18, 18, 18);

                }else if (!firstFrame)
                {
                    sprite.TextureRect = new IntRect(54, 18, 18, 18);
                }
            }

            if (frozenTimer <= 0.0f)
            {
                if (firstFrame)
                {
                    sprite.TextureRect = new IntRect(36, 0, 18, 18);

                }else if (!firstFrame)
                {
                    sprite.TextureRect = new IntRect(54, 0, 18, 18);
                }
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Animation(deltaTime);
            base.Update(scene, deltaTime);
            frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
    }
}
