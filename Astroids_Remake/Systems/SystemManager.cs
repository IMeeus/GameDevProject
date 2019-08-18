using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Systems
{
    public interface ISystemManager
    {
        void AddSystem(ISystem newSystem);
        void Update(float deltaTime);
    }

    /// <summary>
    /// Allows for multiple systems to be added and updated together.
    /// </summary>
    public class SystemManager : ISystemManager
    {
        private readonly List<ISystem> _systems;

        public SystemManager()
        {
            _systems = new List<ISystem>();
        }

        /// <summary>
        /// Adds a system to the manager.
        /// </summary>
        /// <param name="newSystem">The new system that is added to the manager.</param>
        public void AddSystem(ISystem newSystem)
        {
            _systems.Add(newSystem);
        }

        /// <summary>
        /// Updates all the systems in the manager.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            foreach (ISystem system in _systems)
                system.Update(deltaTime);
        }
    }
}
