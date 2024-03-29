﻿using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Graphicals
{
    public class TextField
    {
        private float _scale;
        private Color _color;
        private Vector2 _position;
        private SpriteFont _font;

        public TextField(SpriteFont font, Vector2 position, float scale, Color color)
        {
            _scale = scale;
            _color = color;
            _position = position;
            _font = font;
        }

        /// <summary>
        /// Draws the scoreboard with the given score as a parameter.
        /// </summary>
        /// <param name="score">The score which you want to draw.</param>
        /// <param name="spriteBatch">The spritebatch that is used to draw on the screen.</param>
        public void Draw(SpriteBatch spriteBatch, string text, bool centered)
        {
            Vector2 origin = Vector2.Zero;

            if (centered)
                origin = new Vector2(_font.MeasureString(text).X / 2, _font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(_font, text, _position, _color, 0f, origin, _scale, SpriteEffects.None, LayerDepth.OVERLAY);
        }
    }
}
