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
            loaders = new Dictionary<char, Func<Entity>>{{'#', () => new Wall()}};
        }

        public void Load(string scene) => nextScene = scene;

        public void Reload() => nextScene = currentScene;

        public void HandleSceneLoad(Scene scene)
        {
            if (nextScene == "") return;
            scene.Clear();




            
            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}'");

            //Read the file

            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                string parsed = line.Trim();

                parsed = parsed.Substring(1, 18);

                if (parsed.Length < 1) continue;
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
