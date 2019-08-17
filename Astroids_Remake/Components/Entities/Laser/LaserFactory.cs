using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Entities.Laser
{
    public interface ILaserFactory
    {
        void SpawnLaser(LaserType type, Vector2 position, float rotation);
    }

    /// <summary>
    /// Controls the creation of laser entities.
    /// </summary>
    public class LaserFactory : ILaserFactory
    {
        private readonly IEntityManager _entityManager;

        public LaserFactory(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        /// <summary>
        /// Adds a new laser to the entityManager.
        /// </summary>
        /// <param name="type">Het type laser</param>
        /// <param name="position">De positie van de laser</param>
        /// <param name="rotation">De richting/rotatie van de laser</param>
        public void SpawnLaser(LaserType type, Vector2 position, float rotation)
        {
            Laser laser = null;

            switch (type)
            {
                case LaserType.LIGHT: laser = new WeakLaser(position, rotation); break;
                case LaserType.MEDIUM: laser = new MediumLaser(position, rotation); break;
                case LaserType.STRONG: laser = new StrongLaser(position, rotation); break;
            }

            _entityManager.AddEntity(laser);
        }
    }
}
