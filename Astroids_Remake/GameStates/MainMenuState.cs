using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astroids_Remake.Graphicals.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroids_Remake.GameStates
{
    /// <summary>
    /// Describes the behaviour of the game in its menu state.
    /// </summary>
    public class MainMenuState : GameState
    {
        private Menu _menu;

        public MainMenuState(IGame game) : base(game) { }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            _menu = new MainMenu(_game.Center, _game.Content.Load<SpriteFont>("font"), 3, 10, _game);
        }

        public override void Update(float deltaTime)
        {
            _menu.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            _menu.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
