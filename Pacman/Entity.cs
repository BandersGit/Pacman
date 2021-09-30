using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman
{
    public class Entity
    {
        private string textureName;
        protected Sprite sprite;
        public bool Dead;
        public bool DontDestroyOnLoad;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual FloatRect Bounds => sprite.GetGlobalBounds();

        public virtual bool Solid => false;

        protected virtual void CollideWith(Scene scene, Entity other)
        {

        }

        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }
        
        public virtual void Destroy(Scene scene)
        {
            
        }

        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach(Entity found in scene.FindInstersects(Bounds))
            {
                CollideWith(scene, found);
            }
        }

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }

        

        
    }
}
