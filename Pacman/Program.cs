using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(828, 900), "Pacman"))
            {
                window.Closed += (o, e) => window.Close();

                //Initialize
                Clock clock = new Clock();
                Scene scene = new Scene();
                scene.Loader.Load("maze");

                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();

                    //Update

                    window.Clear(new Color(223, 246, 245));
                    //Draw
                    window.Display();
                }
            }
        }
    }
}
