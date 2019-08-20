using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Graphicals
{
    public interface IBackground
    {
        Texture2D Texture { get; }
        Vector2 Position { get; }
        void Draw(SpriteBatch spriteBatch);
    }

    /// <summary>
    /// Allows a background image to be drawn on the screen at the desired position.
    /// </summary>
    public class Background : IBackground
    {
        private readonly ScreenLocation _location;
        private readonly IGameDimensions _gameDimensions;

        public Background(Texture2D texture, ScreenLocation location, IGameDimensions gameDimensions)
        {
            _location = location;
            _gameDimensions = gameDimensions;

            Texture = texture;
        }

        public Texture2D Texture { get; private set; }
        public Vector2 Position
        {
            get
            {
                switch (_location)
                {
                    case ScreenLocation.CENTER: return new Vector2(_gameDimensions.Center.X - (Texture.Width / 2), _gameDimensions.Center.Y - (Texture.Height / 2));
                    case ScreenLocation.TOPLEFT: return new Vector2(0, 0);
                    case ScreenLocation.TOPRIGHT: return new Vector2(_gameDimensions.ScreenWidth - Texture.Width, 0);
                    case ScreenLocation.BOTTOMLEFT: return new Vector2(0, _gameDimensions.ScreenHeight - Texture.Height);
                    case ScreenLocation.BOTTOMRIGHT: return new Vector2(_gameDimensions.ScreenWidth - Texture.Width, _gameDimensions.ScreenHeight - Texture.Height);
                }
                throw new NotImplementedException("The position for this screenlocation has not been implemented.");
            }
        }

        /// <summary>
        /// Draws the background image on the screen.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw on the screen.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                Position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                LayerDepth.BACKGROUND);
        }
    }
}