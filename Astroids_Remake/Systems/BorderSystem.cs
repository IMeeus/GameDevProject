using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Components.Entities.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Systems
{
    // It is beter to let the individual classed implement this behaviour. (Single Responsibility Principle)
    // Refactor to OutOfBounds Behaviour for each individual class!
    /// <summary>
    /// A class that defines the behaviour of entities that go out of the game borders.
    /// </summary>
    public class BorderSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly IGameDimensions _gameDimensions;

        public BorderSystem(IEntityManager entityManager, IGameDimensions gameDimensions)
        {
            _entityManager = entityManager;
            _gameDimensions = gameDimensions;
        }

        public void Update(float deltaTime)
        {
            Player player = (Player)_entityManager.Entities.SingleOrDefault(e => e is Player);
            List<Meteor> meteors = _entityManager.Entities.Where(e => e is Meteor).Cast<Meteor>().ToList();
            List<Laser> lasers = _entityManager.Entities.Where(e => e is Laser).Cast<Laser>().ToList();

            if (player != null)
                HandlePlayerOutOfBounds(player);
            foreach(Meteor meteor in meteors)
                HandleMeteorOutOfBounds(meteor);
            foreach (Laser laser in lasers)
                HandleLaserOutOfBounds(laser);
        }

        /// <summary>
        /// Handles the behaviour of a player that goes out of bounds.
        /// </summary>
        /// <param name="player">The player that may go out of bounds.</param>
        private void HandlePlayerOutOfBounds(Player player)
        {
            if (player.Direction.X < 0 && player.BoundingCircle.Right < 0)
                player.Position = new Vector2(_gameDimensions.ScreenWidth, player.Position.Y);
            if (player.Direction.X > 0 && player.BoundingCircle.Left > _gameDimensions.ScreenWidth)
                player.Position = new Vector2(0 - player.BoundingCircle.Diameter, player.Position.Y);
            if (player.Direction.Y < 0 && player.BoundingCircle.Bottom < 0)
                player.Position = new Vector2(player.Position.X, _gameDimensions.ScreenHeight);
            if (player.Direction.Y > 0 && player.BoundingCircle.Top > _gameDimensions.ScreenHeight)
                player.Position = new Vector2(player.Position.X, 0 - player.BoundingCircle.Diameter);
        }

        /// <summary>
        /// Handles the behaviour of a meteor that goes out of bounds.
        /// </summary>
        /// <param name="meteor">The meteor that may go out of bounds.</param>
        private void HandleMeteorOutOfBounds(Meteor meteor)
        {
            if (meteor.Direction.X < 0 && meteor.BoundingCircle.Right < 0)
                meteor.Position = new Vector2(_gameDimensions.ScreenWidth, meteor.Position.Y);
            if (meteor.Direction.X > 0 && meteor.BoundingCircle.Left > _gameDimensions.ScreenWidth)
                meteor.Position = new Vector2(0 - meteor.BoundingCircle.Diameter, meteor.Position.Y);
            if (meteor.Direction.Y < 0 && meteor.BoundingCircle.Bottom < 0)
                meteor.Position = new Vector2(meteor.Position.X, _gameDimensions.ScreenHeight);
            if (meteor.Direction.Y > 0 && meteor.BoundingCircle.Top > _gameDimensions.ScreenHeight)
                meteor.Position = new Vector2(meteor.Position.X, 0 - meteor.BoundingCircle.Diameter);
        }

        /// <summary>
        /// Handles the behaviour of a laser that goes out of bounds.
        /// </summary>
        /// <param name="laser">The laser that may go out of bounds.</param>
        private void HandleLaserOutOfBounds(Laser laser)
        {
            if (laser.Direction.X < 0 && laser.BoundingRectangle.Right < 0)
                laser.Position = new Vector2(_gameDimensions.ScreenWidth, laser.Position.Y);
            if (laser.Direction.X > 0 && laser.BoundingRectangle.Left > _gameDimensions.ScreenWidth)
                laser.Position = new Vector2(0 - laser.BoundingRectangle.Width, laser.Position.Y);
            if (laser.Direction.Y < 0 && laser.BoundingRectangle.Bottom < 0)
                laser.Position = new Vector2(laser.Position.X, _gameDimensions.ScreenHeight);
            if (laser.Direction.Y > 0 && laser.BoundingRectangle.Top > _gameDimensions.ScreenHeight)
                laser.Position = new Vector2(laser.Position.X, 0 - laser.BoundingRectangle.Width);
        }
    }
}
