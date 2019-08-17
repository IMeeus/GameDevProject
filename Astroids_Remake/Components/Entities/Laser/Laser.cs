using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroids_Remake.Components.Entities.Laser
{
    /// <summary>
    /// Enumeration of laser types.
    /// </summary>
    public enum LaserType
    {
        LIGHT, MEDIUM, STRONG
    }

    /// <summary>
    /// Describes a laser entity.
    /// </summary>
    public abstract class Laser : Entity
    {
        public Laser(Vector2 position, float rotation)
        {
            Rotation = rotation;
            TimeToLive = 1f;
            Position = position;
        }

        public int Damage { get; protected set; }
        public float Rotation { get; private set; }
        public float LinearVelocity { get; protected set; }
        public float TimeToLive { get; private set; }
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin => new Vector2(Texture.Width / 2, Texture.Height / 2);
        public Vector2 Direction => new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - Rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - Rotation));

        public override void Update(float deltaTime)
        {
            Position += Direction * LinearVelocity * deltaTime;

            TimeToLive -= deltaTime;
            if (TimeToLive <= 0)
                Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                Position,
                null,
                Color.White,
                Rotation,
                Origin,
                1,
                SpriteEffects.None,
                LayerDepth.MAIN);
        }
    }

    /// <summary>
    /// Describes a weak laser entity.
    /// </summary>
    public class WeakLaser : Laser
    {
        public WeakLaser(Vector2 position, float rotation) : base(position, rotation)
        {
            Damage = 1;
            LinearVelocity = 500f;
            Texture = TextureHolder.LaserTextures["tiny"];
        }
    }

    /// <summary>
    /// Describes a medium laser entity.
    /// </summary>
    public class MediumLaser : Laser
    {
        public MediumLaser(Vector2 position, float rotation) : base(position, rotation)
        {
            Damage = 2;
            LinearVelocity = 750f;
            Texture = TextureHolder.LaserTextures["medium"];
        }
    }

    /// <summary>
    /// Describes a strong laser entity.
    /// </summary>
    public class StrongLaser : Laser
    {
        public StrongLaser(Vector2 position, float rotation) : base(position, rotation)
        {
            Damage = 3;
            LinearVelocity = 1000f;
            Texture = TextureHolder.LaserTextures["strong"];
        }
    }
}
