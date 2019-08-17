using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Extra
{
    /// <summary>
    /// Een static class die alle game textures bevat. In de eerste LoadContent call kan men in 1 keer alle textures in deze holder steken.
    /// Ik maakte deze klasse voor performance redenen. Content.Load() kan best enkel in de LoadContent() methode worden aangeroepen.
    /// Deze klasse zorgt ervoor dat alle textures altijd overal beschikbaar zijn zonder dependencies te hoeven injecteren.
    /// Maakt het ook gemakkelijker om met subklassen te werken die elks een andere texture property hebben.
    /// </summary>
    public static class TextureHolder
    {
        public static Texture2D PlayerTexture { get; set; }
        public static Dictionary<string, Texture2D> LaserTextures { get; set; } = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> MeteorTextures { get; set; } = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> BackgroundTextures { get; set; } = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Maakt alle properties leeg.
        /// </summary>
        public static void Clear()
        {
            PlayerTexture = null;
            LaserTextures.Clear();
            MeteorTextures.Clear();
            BackgroundTextures.Clear();
        }
    }
}
