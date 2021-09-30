using System.Collections.Generic;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);

    public class Scene
    {
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent EatCandy;

        private List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly AssetManager Assets;
        private int scoreGained;
        private int healthLost;
        private int candiesEaten;

        public void PublishGainScore(int amount) => scoreGained += amount;
        public void PublishLoseHealth(int amount) => healthLost += amount;
        public void PublishEatCandy(int amount) => candiesEaten += amount;

        public Scene()
        {
            entities = new List<Entity>();
            Loader = new SceneLoader();
            Assets = new AssetManager();
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
                if (entity is T typed)
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

            if (scoreGained != 0)
            {
                GainScore?.Invoke(this, scoreGained);
                scoreGained = 0;
            }

            if (healthLost != 0)
            {
                LoseHealth?.Invoke(this, healthLost);
                healthLost = 0;
            }

            if (candiesEaten != 0)
            {
                EatCandy?.Invoke(this, candiesEaten);
                candiesEaten = 0;
            }

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
