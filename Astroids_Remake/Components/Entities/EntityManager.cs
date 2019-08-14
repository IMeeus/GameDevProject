using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities
{
    /// <summary>
    /// Manages all the game entities.
    /// </summary>
    public class EntityManager
    {
        private List<Entity> _entities;

        public EntityManager()
        {
            _entities = new List<Entity>();
        }

        public IEnumerable<Entity> Entities => _entities;

        /// <summary>
        /// Updates all the entities that haven't been destoyed.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            foreach (var entity in _entities.Where(e => !e.IsDestroyed))
                entity.Update(deltaTime);

            _entities.RemoveAll(e => e.IsDestroyed);
        }

        /// <summary>
        /// Draws all the entities that haven't been destroyed.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw to the screen.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities.Where(e => !e.IsDestroyed))
                entity.Draw(spriteBatch);
        }

        public void AddEntity(Entity newEntity)
        {
            _entities.Add(newEntity);
        }

        /// <summary>
        /// Removes all the entities.
        /// </summary>
        public void Clear()
        {
            _entities.Clear();
        }
    }
}
