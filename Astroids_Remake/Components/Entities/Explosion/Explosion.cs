using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astroids_Remake.Extra;
using Astroids_Remake.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroids_Remake.Components.Entities.Explosion
{
    public class Explosion : Entity
    {
        private IAnimation _animation;

        public float Radius { get; private set; }
        public Vector2 Position { get; private set; }

        public Explosion(Vector2 position, float radius)
        {
            InitializeAnimation();
            Radius = radius;
            Position = position;
        }

        /// <summary>
        /// Initializes the animation attribute.
        /// </summary>
        private void InitializeAnimation()
        {
            _animation = new Animation(TextureHolder.Textures["explosion"], 20, false);

            _animation.AddFrame(new Rectangle(25, 20, 150, 150));
            _animation.AddFrame(new Rectangle(217, 20, 150, 150));
            _animation.AddFrame(new Rectangle(417, 20, 150, 150));
            _animation.AddFrame(new Rectangle(614, 20, 150, 150));
            _animation.AddFrame(new Rectangle(808, 20, 150, 150));
            _animation.AddFrame(new Rectangle(1003, 20, 150, 150));
            _animation.AddFrame(new Rectangle(1205, 20, 150, 150));
            _animation.AddFrame(new Rectangle(1596, 20, 150, 150));
            _animation.AddFrame(new Rectangle(1789, 20, 150, 150));

            _animation.Start();
        }

        public override void Update(float deltaTime)
        {
            _animation.Update(deltaTime);
            if (!_animation.Playing)
                Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _animation.Texture,
                Position,
                _animation.CurrentFrame,
                Color.White,
                0f,
                new Vector2(_animation.CurrentFrame.Width / 2, _animation.CurrentFrame.Height / 2),
                (Radius * 2) / _animation.CurrentFrame.Width,
                SpriteEffects.None,
                LayerDepth.MAIN);
        }
    }
}
