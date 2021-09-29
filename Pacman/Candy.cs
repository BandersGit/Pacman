using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
    }
}