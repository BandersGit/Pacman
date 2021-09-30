using System;
using System.Collections.Generic;
using SFML.Graphics;


namespace Pacman
{
    public class AssetManager
    {
        public static readonly string AssetPath = "assets";
        private readonly Dictionary<string, Texture> textures;
        private readonly Dictionary<string, Font> fonts;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
        }

        public Texture LoadTexture(string textureName)
        {
            if (textures.TryGetValue(textureName, out Texture found))
            {
                return found;
            }

            string fileName = $"{AssetPath}/{textureName}.png";
            Texture texture = new Texture(fileName);
            textures.Add(textureName, texture);
            return texture;
        }

        public Font LoadFont(string fontName)
        {
            if (fonts.TryGetValue(fontName, out Font found))
            {
                return found;
            }

            string fileName = $"{AssetPath}/{fontName}.ttf";
            Font font = new Font(fileName);
            fonts.Add(fontName, font);
            return font;
        }
    }
}
