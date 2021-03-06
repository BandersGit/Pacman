using System;
using System.Collections.Generic;
using SFML.Graphics;


namespace Pacman
{
    public class Scene
    {
        
        private List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly AssetManager Assets;
        public readonly EventManager Events;

        public Scene()
        {
            entities = new List<Entity>();
            Loader = new SceneLoader();
            Assets = new AssetManager();
            Events = new EventManager();
        }

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public void Clear()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];

                if (!entity.DontDestroyOnLoad)
                {
                    entities.RemoveAt(i);
                    entity.Destroy(this);
                }
            }
        }

        public IEnumerable<Entity> FindInstersects(FloatRect bounds)
        {
            int lastEntity = entities.Count - 1;

            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = entities[i];

                if (entity.Dead) continue;

                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }

        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach (Entity entity in entities)
            {
                if (entity is T typed && !entity.Dead)
                {
                    found = typed;
                    return true;
                }
            }

            found = default(T);
            return false;
        }

        public void UpdateAll(float deltaTime)
        {
            Loader.HandleSceneLoad(this);

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }

            Events.HandleEvents(this);

            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];

                if (entity.Dead)
                {
                    entities.RemoveAt(i);
                }else
                i++;
            }
        }

        public void RenderAll(RenderTarget target)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];
                entity.Render(target);
            }
        }
    
    }
}
