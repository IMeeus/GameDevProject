using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Explosion;
using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Components.Entities.Player;
using Astroids_Remake.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Systems
{
    public class CollisionSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly IMeteorFactory _meteorFactory;

        public CollisionSystem(IEntityManager entityManager, IMeteorFactory meteorFactory)
        {
            _entityManager = entityManager;
            _meteorFactory = meteorFactory;
        }

        public void Update(float deltaTime)
        {
            Player player = (Player)_entityManager.Entities.SingleOrDefault(e => e is Player);
            List<Meteor> meteors = _entityManager.Entities.Where(e => e is Meteor).Cast<Meteor>().ToList();
            List<Laser> lasers = _entityManager.Entities.Where(e => e is Laser).Cast<Laser>().ToList();

            foreach (Meteor meteor in meteors)
            {
                if (player != null)
                    HandlePlayerMeteorCollision(player, meteor);

                foreach (Laser laser in lasers)
                    HandleLaserMeteorCollision(laser, meteor);

                if (meteor.IsDestroyed)
                    HandleMeteorDestuction(meteor);
            }
        }

        /// <summary>
        /// Handles the collision between the player and a meteor.
        /// </summary>
        /// <param name="player">The player that may collide.</param>
        /// <param name="meteor">The meteor that may collide.</param>
        private void HandlePlayerMeteorCollision(Player player, Meteor meteor)
        {
            if (CollisionDetector.CheckCollision(player.BoundingCircle, meteor.BoundingCircle))
            {
                player.Damage(1);
                meteor.Destroy();
                _entityManager.AddEntity(new Explosion(meteor.Position, meteor.BoundingCircle.Radius));
            }
        }

        /// <summary>
        /// Handles the collision between a laser and a meteor.
        /// </summary>
        /// <param name="laser">The laser that may collide.</param>
        /// <param name="meteor">The meteor that may collide.</param>
        private void HandleLaserMeteorCollision(Laser laser, Meteor meteor)
        {
            if (CollisionDetector.CheckCollision(laser.BoundingRectangle, meteor.BoundingCircle))
            {
                laser.Destroy();
                meteor.Damage(laser.Damage);
                _entityManager.AddEntity(new Explosion(meteor.Position, meteor.BoundingCircle.Radius));
            }
        }

        /// <summary>
        /// Handles the destruction of a meteor.
        /// </summary>
        /// <param name="meteor">The meteor that has been destroyed.</param>
        private void HandleMeteorDestuction(Meteor meteor)
        {
            if (meteor is TinyMeteor) return;
            _meteorFactory.SplitMeteor(meteor, true);
        }
    }
}
