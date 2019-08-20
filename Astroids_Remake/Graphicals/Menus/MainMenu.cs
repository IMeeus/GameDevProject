using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astroids_Remake.GameLogic.Input;
using Astroids_Remake.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroids_Remake.Graphicals.Menus
{
    /// <summary>
    /// An implementation of the Menu Class.
    /// </summary>
    public class MainMenu : Menu
    {
        private readonly IGame _game;

        public MainMenu(Vector2 position, SpriteFont font, float textScale, int marginBetweenItems, IGame game) : base(position, font, textScale, marginBetweenItems, game.Input)
        {
            _game = game;
        }

        protected override void LoadItems()
        {
            AddItem("play", "PLAY");
            AddItem("settings", "SETTINGS");
            AddItem("exit", "EXIT");
        }

        protected override void SelectItem()
        {
            switch (SelectedItem)
            {
                case "play": GoToGame(); break;
                case "settings": GoToSettingsMenu(); break;
                case "exit": ExitGame(); break;
            }
        }

        private void GoToGame()
        {
            _game.SetState(new PlayingState(_game));
        }

        private void GoToSettingsMenu()
        {
            _game.SetState(new SettingsState(_game));
        }

        private void ExitGame()
        {
            _game.Exit();
        }
    }
}
