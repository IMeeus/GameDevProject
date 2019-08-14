using Astroids_Remake.GameLogic.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities
{
    /// <summary>
    /// Describes the behaviour of a player entity.
    /// </summary>
    public class Player : Entity
    {
        private readonly Input _input;

        public Player(Texture2D texture, Input input)
        {
            _input = input;

            Texture = texture;
            Rotation = 0f;
            ShootCooldown = 0f;
            LinearVelocity = 200f;
            RotationVelocity = 180f;
        }

        public float Rotation { get; private set; }
        public float ShootCooldown { get; private set; }
        public float LinearVelocity { get; private set; }
        public float RotationVelocity { get; private set; }
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin => new Vector2(Texture.Width / 2, Texture.Height / 2);
        public Vector2 Direction => new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - Rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - Rotation));

        public override void Update(float deltaTime)
        {
            if (ShootCooldown > 0)
                ShootCooldown -= deltaTime;

            HandleInput(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                Rotation,
                Origin,
                1,
                SpriteEffects.None,
                0);
        }

        /// <summary>
        /// Handles the input for the player entity.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        private void HandleInput(float deltaTime)
        {
            if (_input.LeftHold)
                Rotation -= MathHelper.ToRadians(RotationVelocity * deltaTime);
            if (_input.RightHold)
                Rotation += MathHelper.ToRadians(RotationVelocity * deltaTime);
            if (_input.UpHold)
                Position += Direction * LinearVelocity * deltaTime;
            if (_input.DownHold)
                Position -= Direction * LinearVelocity * deltaTime;
            if (_input.ShootHold)
                Shoot();
        }

        /// <summary>
        /// Lets the player shoot a laser if the cooldown is 0.
        /// </summary>
        public void Shoot()
        {
            if (ShootCooldown > 0)
                return;

            ShootCooldown = .2f;
        }
    }
}
