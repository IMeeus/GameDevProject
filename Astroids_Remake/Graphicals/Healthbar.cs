using Astroids_Remake.Components.Entities.Player;
using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Graphicals.Overlay
{
    /// <summary>
    /// Describes a healthbar that represents the health of a player entity.
    /// </summary>
    public class Healthbar
    {
        private readonly Player _player;
        private readonly Texture2D _texture;
        private float _healthPercentage;
        private Color _color;
        private Rectangle _destinationRectangle;
        private Rectangle _sourceRectangle;

        public Healthbar(Texture2D texture, Color color, Rectangle destinationRectangle, Player player)
        {
            _player = player;
            _texture = texture;
            _color = color;
            _destinationRectangle = destinationRectangle;
            _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        /// <summary>
        /// Keeps the healthbar updated with the player's health.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            _healthPercentage = (float)_player.Health / (float)_player.MaxHealth;
        }

        /// <summary>
        /// Draws the healthbar on the screen.
        /// The healthbar's color has a hardcoded alpha value of 100.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw on the screen.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle(_destinationRectangle.X, _destinationRectangle.Y, (int)(_destinationRectangle.Width * _healthPercentage), _destinationRectangle.Height);
            Rectangle sourceRectangle = new Rectangle(_sourceRectangle.X, _sourceRectangle.Y, (int)(_sourceRectangle.Width * _healthPercentage), _sourceRectangle.Height);
            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, _color, 0f, Vector2.Zero, SpriteEffects.None, LayerDepth.OVERLAY);
        }
    }
}
