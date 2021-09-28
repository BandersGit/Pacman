using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman
{
    public class SceneLoader
    {
        private readonly Dictionary<char, Func<Entity>> loaders;
        private string currentScene = "", nextScene = "";

        public SceneLoader()
        {
            loaders = new Dictionary<char, Func<Entity>>
            {
                {'#', () => new Wall()}
            };

        }

        public void Load(string scene) => nextScene = scene;

        public void Reload() => nextScene = currentScene;

        public void HandleSceneLoad(Scene scene)
        {
            if (nextScene == "") return;
            scene.Clear();




            
            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}'");
            string[] contents = File.ReadAllLines(file, Encoding.UTF8);
            //Read the file

            for (int i = 0; i < contents.Length; i++)
            {
                for (int j = 0; j < contents[i].Length; j++)
                {
                    if (!Create(contents[i][j], out Entity entity)) continue;
                    
                    
                    entity.Position = new Vector2f(j * 18, i * 18);
                    scene.Spawn(entity);
                }
            }






            currentScene = nextScene;
            nextScene = "";
        }

        private bool Create(char symbol, out Entity created)
        {
            if (loaders.TryGetValue(symbol, out Func<Entity> loader))
            {
                created = loader();
                return true;
            }

            created = null;
            return false;
        }
    }
}
