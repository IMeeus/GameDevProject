using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities
{
    public interface IEntityManager
    {
        List<Entity> Entities { get; }

        void AddEntity(Entity newEntity);
        void Update(float deltaTime);
        void Draw(SpriteBatch spriteBatch);
        void Clear();
    }

    /// <summary>
    /// Manages all the game entities.
    /// </summary>
    public class EntityManager : IEntityManager
    {
        public EntityManager()
        {
            Entities = new List<Entity>();
        }

        public List<Entity> Entities { get; private set; }

        public void AddEntity(Entity newEntity)
        {
            Entities.Add(newEntity);
        }

        /// <summary>
        /// Updates all the entities that haven't been destoyed.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            foreach (var entity in Entities.Where(e => !e.IsDestroyed).ToList())
                entity.Update(deltaTime);

            Entities.RemoveAll(e => e.IsDestroyed);
        }

        /// <summary>
        /// Draws all the entities that haven't been destroyed.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw to the screen.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in Entities.Where(e => !e.IsDestroyed).ToList())
                entity.Draw(spriteBatch);
        }

        /// <summary>
        /// Removes all the entities.
        /// </summary>
        public void Clear()
        {
            Entities.Clear();
        }
    }
}
