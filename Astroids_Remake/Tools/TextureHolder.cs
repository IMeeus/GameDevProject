using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Tools
{
    /// <summary>
    /// You can use this static class to load all your game textures and make them globally accessible.
    /// I made this class to optimize the performance. Content.Load() shouldn't be called in Update and Draw methods.
    /// Therefore it is better to load the textures beforehand and access them later.
    /// </summary>
    public static class TextureHolder
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; } = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Adds a texture to the textureHolder.
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="texture">The texture that you want to add.</param>
        public static void AddTexture(string name, Texture2D texture)
        {
            Textures.Add(name, texture);
        }

        /// <summary>
        /// Removes all the textures from the textureHolder.
        /// </summary>
        public static void Clear()
        {
            Textures.Clear();
        }
    }
}
