using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.GameStates
{
    /// <summary>
    /// Holds the logic for a certain game state.
    /// The game can have many states and switch between them to change its behaviour.
    /// </summary>
    public abstract class GameState
    {
        protected readonly IGame _game;

        public GameState(IGame game)
        {
            _game = game;
        }

        /// <summary>
        /// Allows the gamestate to perform any initialization it needs to before starting to run.
        /// Will be called once every time the game changes to this state.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// This is the place to load all the gamestate specific content.
        /// Will be called once every time the game changes to this state.
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Allows the gamestate to run logic.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw to the screen.</param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
