using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities.Meteor
{
    public interface IMeteorFactory
    {
        Meteor CreateMeteor(MeteorType type, bool inject);
        Meteor CreateMeteor(MeteorType type, Vector2 position, Vector2 direction, bool inject);
        Meteor CreateMeteor(MeteorType type, Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity, bool inject);
        List<Meteor> SplitMeteor(Meteor meteor, bool inject);
    }

    /// <summary>
    /// Controls the creation of meteor entities
    /// </summary>
    public class MeteorFactory : IMeteorFactory
    {
        private readonly IEntityManager _entityManager;
        private readonly Random _random;
        private readonly IGameDimensions _gameSize;

        public MeteorFactory(IEntityManager entityManager, IGameDimensions gameSize)
        {
            _entityManager = entityManager;
            _random = new Random();
            _gameSize = gameSize;
        }

        /// <summary>
        /// Creates a meteor entity with a random position, direction, linear and rotation velocity.
        /// </summary>
        /// <param name="type">Type of the meteor.</param>
        /// <param name="inject">Whether the meteor should be injected into the entity manager.</param>
        /// <returns>Th new meteor entity.</returns>
        public Meteor CreateMeteor(MeteorType type, bool inject)
        {
            Vector2 randomPosition = new Vector2(_random.Next(0, _gameSize.ScreenWidth), _random.Next(0, _gameSize.ScreenHeight));
            Vector2 randomDirection = VectorHelper.AngleToVector((float)(_random.NextDouble() * Math.PI * 2));

            return CreateMeteor(type, randomPosition, randomDirection, inject);
        }

        /// <summary>
        /// Creates a new meteor entity with a random linear and rotation velocity.
        /// </summary>
        /// <param name="type">The type of the new meteor</param>
        /// <param name="position">The position of the new meteor</param>
        /// <param name="direction">The direction of the new meteor</param>
        /// <param name="inject">Whether the meteor should be injected into the entity manager.</param>
        /// <returns>The new meteor entity.</returns>
        public Meteor CreateMeteor(MeteorType type, Vector2 position, Vector2 direction, bool inject)
        {
            float linearVelocity = _random.Next(100, 200);
            float rotationVelocity = _random.Next(90, 360);

            return CreateMeteor(type, position, direction, linearVelocity, rotationVelocity, inject);
        }

        /// <summary>
        /// Creates a new meteor entity.
        /// </summary>
        /// <param name="type">The type of the new meteor</param>
        /// <param name="position">The position of the new meteor</param>
        /// <param name="direction">The direction of the new meteor</param>
        /// <param name="linearVelocity">The linear velocity of the new meteor</param>
        /// <param name="rotationVelocity">The rotation velocity of the new meteor</param>
        /// 
        /// <returns>The new meteor entity.</returns>
        public Meteor CreateMeteor(MeteorType type, Vector2 position, Vector2 direction, float linearVelocity, float rotationVelocity, bool inject)
        {
            Meteor newMeteor = null;
            switch (type)
            {
                case MeteorType.TINY: newMeteor = new TinyMeteor(position, direction, linearVelocity, rotationVelocity); break;
                case MeteorType.SMALL: newMeteor = new SmallMeteor(position, direction, linearVelocity, rotationVelocity); break;
                case MeteorType.MEDIUM: newMeteor = new MediumMeteor(position, direction, linearVelocity, rotationVelocity); break;
                case MeteorType.BIG: newMeteor = new BigMeteor(position, direction, linearVelocity, rotationVelocity); break;
            }

            if (inject) _entityManager.AddEntity(newMeteor);

            return newMeteor;
        }

        /// <summary>
        /// Splits a meteor entity into 2 smaller meteors entities.
        /// </summary>
        /// <param name="meteor">The meteor that has to be split.</param>
        /// <param name="inject">Whether the smaller meteors should be injected into the entity manager.</param>
        public List<Meteor> SplitMeteor(Meteor meteor, bool inject)
        {
            if (meteor is TinyMeteor) throw new InvalidOperationException("Can't split a meteor of type TinyMeteor");

            List<Meteor> newMeteors = new List<Meteor>();

            float currentDirection = VectorHelper.VectorToAngle(meteor.Direction);
            Vector2 newDirection = Vector2.Zero;

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    newDirection = VectorHelper.AngleToVector(currentDirection + (float)(Math.PI / 2));
                else
                    newDirection = VectorHelper.AngleToVector(currentDirection - (float)(Math.PI / 2));

                if (meteor is SmallMeteor)
                    newMeteors.Add(CreateMeteor(MeteorType.TINY, meteor.Position, newDirection, inject));
                if (meteor is MediumMeteor)
                    newMeteors.Add(CreateMeteor(MeteorType.SMALL, meteor.Position, newDirection, inject));
                if (meteor is BigMeteor)
                    newMeteors.Add(CreateMeteor(MeteorType.BIG, meteor.Position, newDirection, inject));
            }

            return newMeteors;
        }
    }
}