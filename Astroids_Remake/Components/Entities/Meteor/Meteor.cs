using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities.Meteor
{
    /// <summary>
    /// Enumeration of meteor types.
    /// </summary>
    public enum MeteorType
    {
        TINY, SMALL, MEDIUM, BIG
    }

    public interface ISplitable
    {
        void Split();
    }

    /// <summary>
    /// Describes a meteor entity.
    /// </summary>
    public abstract class Meteor : Entity
    {
        public Meteor(Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity)
        {
            Position = position;
            Direction = direction;
            LinearVelocity = linearVelocity;
            RotationVelocity = rotationVelocity;
        }

        public int Health { get; protected set; }
        public float Radius { get; protected set; }
        public float Rotation { get; private set; }
        public float LinearVelocity { get; private set; }
        public float RotationVelocity { get; private set; }
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; private set; }
        public Vector2 Origin => new Vector2(Texture.Width / 2, Texture.Height / 2);
        public Vector2 Direction { get; set; }

        public override void Update(float deltaTime)
        {
            UpdatePosition(deltaTime);
            UpdateRotation(deltaTime);
        }

        /// <summary>
        /// Updates the position of the meteor.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        private void UpdatePosition(float deltaTime)
        {
            Position += Direction * LinearVelocity * deltaTime;
        }

        /// <summary>
        /// Updates the rotation of the meteor.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        private void UpdateRotation(float deltaTime)
        {
            Rotation += MathHelper.ToRadians(RotationVelocity * deltaTime);
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

        /// <summary>
        /// Damages the meteor.
        /// </summary>
        /// <param name="damage">The amount of damage done.</param>
        public void Damage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
                Destroy();
        }
    }

    /// <summary>
    /// Describes a tiny meteor entity.
    /// </summary>
    public class TinyMeteor : Meteor
    {
        public TinyMeteor(Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity) : base(position, direction, linearVelocity, rotationVelocity)
        {
            Health = 1;
            Radius = 9;
            Texture = TextureHolder.MeteorTextures["tiny"];
        }
    }

    /// <summary>
    /// Describes a small meteor entity.
    /// </summary>
    public class SmallMeteor : Meteor
    {
        public SmallMeteor(Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity) : base(position, direction, linearVelocity, rotationVelocity)
        {
            Health = 2;
            Radius = 14;
            Texture = TextureHolder.MeteorTextures["small"];
        }
    }

    /// <summary>
    /// Describes a medium meteor entity.
    /// </summary>
    public class MediumMeteor : Meteor
    {
        public MediumMeteor(Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity) : base(position, direction, linearVelocity, rotationVelocity)
        {
            Health = 4;
            Radius = 22;
            Texture = TextureHolder.MeteorTextures["medium"];
        }
    }

    /// <summary>
    /// Describes a big meteor entity.
    /// </summary>
    public class BigMeteor : Meteor
    {
        public BigMeteor(Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity) : base(position, direction, linearVelocity, rotationVelocity)
        {
            Health = 8;
            Radius = 50;
            Texture = TextureHolder.MeteorTextures["big"];
        }
    }
}
