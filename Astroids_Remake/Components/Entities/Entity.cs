using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities
{
    /// <summary>
    /// Describes an entity component.
    /// </summary>
    public abstract class Entity
    {
        public bool IsDestroyed { get; private set; }

        public Entity()
        {
            IsDestroyed = false;
        }

        /// <summary>
        /// Updates the entity's properties.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Draws the entity to the screen.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw on the screen.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Destroys the entity
        /// </summary>
        public virtual void Destroy()
        {
            IsDestroyed = true;
        }
    }
}
