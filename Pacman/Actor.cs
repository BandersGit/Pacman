using System;
using System.Linq;
using SFML.Graphics;
using SFML.System;


namespace Pacman
{
    public class Actor : Entity
    {
        private bool wasAligned;
        protected float speed;
        protected int direction;
        protected bool moving;
        protected Vector2f originalPosition;
        protected float originalSpeed;

        protected Actor() : base("pacman")
        {
            //Just uses base implementation of Entity constructor
        }

        protected bool IsAligned => (int) MathF.Floor(Position.X) % 18 == 0 && (int) MathF.Floor(Position.Y) % 18 == 0;

        protected bool IsFree(Scene scene, int dir)
        {
            Vector2f at = Position + new Vector2f(9,9);
            at += 18 * ToVector(dir);
            FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
            return !scene.FindInstersects(rect).Any(e => e.Solid);
        }

        protected static Vector2f ToVector(int dir) //Chooses the direction of the movement based on an int value
        {
            switch (dir)
            {
                case 0: //right
                    return new Vector2f(1, 0); 
                case 1: //up
                    return new Vector2f(0, -1); 
                case 2: //left
                    return new Vector2f(-1, 0); 
                case 3: //down
                    return new Vector2f(0, 1);
            }
            return new Vector2f(0, 0);
        }

        protected virtual int PickDirection(Scene scene)
        {
            return 0;
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);

            originalPosition = Position;
            originalSpeed = speed;
        }

        protected void ResetActor()
        {
            wasAligned = false;
            Position = originalPosition;
            speed = originalSpeed;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);

            if (IsAligned)
            {
                if (!wasAligned)
                {
                    direction = PickDirection(scene);
                }

                if (moving)
                {
                    wasAligned = true;
                }
                
            }else
            {
                wasAligned = false;
            }

            if (!moving) return;

            Position += ToVector(direction) * (speed * deltaTime);

            Position = MathF.Floor(Position.X) switch
            {
                < 0 => new Vector2f(432,Position.Y),
                > 432 => new Vector2f(0, Position.Y),
                _ => Position
            };
        }

        
    }
}
