using System;
using SFML.Graphics;

namespace Pacman
{
    public class Candy : Entity
    {
        public Candy() : base("pacman")
        {
            //Just uses base implementation of Entity constructor
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 54, 18, 18);
        }

        protected override void CollideWith(Scene scene, Entity e)
        {
            if (e is Pacman)
            {
                scene.Events.PublishEatCandy(1);
                this.Dead = true;
            }
        }
    }
}
