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
    public class SettingsState : GameState
    {
        private Menu _menu;

        public SettingsState(IGame game) : base(game)
        {
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            _menu = new SettingsMenu(_game.Center, _game.Content.Load<SpriteFont>("font"), 3f, 10, _game);
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
