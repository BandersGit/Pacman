using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 36, 18, 18);
        }
    }
}
