using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Extra;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Levels
{
    /// <summary>
    /// Describes a level.
    /// The level contains a loading mechanic.
    /// The level can be initialized with a list entities. These entities will be added to the entitymanager once the level is fully loaded.
    /// Subclasses can decide what the conditions are for level completion.
    /// </summary>
    public abstract class Level
    {
        private readonly IBackground _background;
        protected readonly IEntityManager _entityManager;
        protected readonly List<Entity> _entities;

        public Level(IEntityManager entityManager, IBackground background)
        {
            _entityManager = entityManager;
            _background = background;
            _entities = new List<Entity>();

            Loaded = false;
            LoadingTime = 5f;
        }

        public bool Loaded { get; private set; }
        public float LoadingTime { get; private set; }

        /// <summary>
        /// Adds an entity to the level.
        /// </summary>
        /// <param name="entity">The entity that is added to the level.</param>
        public void AddEntity(Entity newEntity)
        {
            _entities.Add(newEntity);
        }

        public virtual void Update(float deltaTime)
        {
            if (!Loaded)
            {
                Load(deltaTime);
                return;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Loaded)
                return;

            _background.Draw(spriteBatch);
        }

        /// <summary>
        /// Loads the level. 
        /// After the level is loaded all the level entities are added to the entitymanager.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        private void Load(float deltaTime)
        {
            LoadingTime -= deltaTime;

            if (LoadingTime <= 0)
            {
                foreach (Entity entity in _entities)
                    _entityManager.AddEntity(entity);
                Loaded = true;
            }
        }

        public abstract bool CheckCompleted();
    }

    /// <summary>
    /// A level that is completed once the entitymanager doesn't contain any more meteors.
    /// </summary>
    public class MeteorLevel : Level
    {
        public MeteorLevel(IEntityManager entityManager, IBackground background) : base(entityManager, background)
        {
        }

        /// <summary>
        /// If the entityManager doesn't contain any more meteors, the level is marked as completed.
        /// </summary>
        /// <returns>Whether the level is completed or not.</returns>
        public override bool CheckCompleted()
        {
            if (!Loaded)
                return false;

            if (_entityManager.Entities.Where(e => e is Meteor).ToList().Count == 0)
                return true;
            return false;
        }
    }
}