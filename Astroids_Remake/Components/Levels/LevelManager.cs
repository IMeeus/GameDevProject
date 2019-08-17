using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Levels
{
    public interface ILevelManager
    {
        Queue<Level> Levels { get; }
        bool AllLevelsCompleted { get; }
        Level CurrentLevel { get; }

        void EnqueueLevel(Level newLevel);
        void Update(float deltaTime);
        void Draw(SpriteBatch spriteBatch);
    }

    /// <summary>
    /// Contains all levels of the game and handles switching between them.
    /// </summary>
    public class LevelManager : ILevelManager
    {
        public LevelManager()
        {
            Levels = new Queue<Level>();
        }

        public Queue<Level> Levels { get; private set; }
        public bool AllLevelsCompleted => Levels.Count == 0;
        public Level CurrentLevel => Levels.First();

        /// <summary>
        /// Adds a level to the levels queue.
        /// </summary>
        /// <param name="newLevel">The new level to add.</param>
        public void EnqueueLevel(Level newLevel)
        {
            Levels.Enqueue(newLevel);
        }

        /// <summary>
        /// Updates the current level.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            if (AllLevelsCompleted) return;

            CurrentLevel.Update(deltaTime);

            if (CurrentLevel.CheckCompleted())
                Levels.Dequeue();
        }

        /// <summary>
        /// Draws the current level.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (AllLevelsCompleted) return;

            CurrentLevel.Draw(spriteBatch);
        }
    }
}
