using System;
using SFML.Graphics;

namespace Pacman
{
    public class Coin : Entity
    {
        public Coin() : base("pacman")
        {
            //Just uses base implementation of Entity constructor
        }

        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(36, 36, 18, 18);
            base.Create(scene);
        }

        protected override void CollideWith(Scene scene, Entity e)
        {
            if (e is Pacman)
            {
                Dead = true;
                scene.Events.PublishGainScore(100);
            }
        }
    }
}
