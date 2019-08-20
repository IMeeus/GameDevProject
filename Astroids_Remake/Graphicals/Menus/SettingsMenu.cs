using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astroids_Remake.GameLogic.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroids_Remake.Graphicals.Menus
{
    public class SettingsMenu : Menu
    {
        private readonly IGame _game;

        public SettingsMenu(Vector2 position, SpriteFont font, float textScale, int marginBetweenItems, IGame game) : base(position, font, textScale, marginBetweenItems, game.Input)
        {
            _game = game;
        }

        protected override void LoadItems()
        {
            AddItem("azerty", "AZERTY CONTROLS");
            AddItem("qwerty", "QWERTY CONTROLS");
            AddItem("arrows", "ARROW CONTROLS");
        }

        protected override void SelectItem()
        {
            switch (SelectedItem)
            {
                case "azerty": ChangeInputToAzerty(); break;
                case "qwerty": ChangeInputToQwerty(); break;
                case "arrows": ChangeInputToArrows(); break;
            }

            GoToMainMenu();
        }

        private void ChangeInputToAzerty()
        {
            _game.Input.SetControls(new AzertyControls());
        }

        private void ChangeInputToQwerty()
        {
            _game.Input.SetControls(new QwertyControls());
        }

        private void ChangeInputToArrows()
        {
            _game.Input.SetControls(new ArrowControls());
        }

        private void GoToMainMenu()
        {
            _game.SetState("mainMenu");
        }
    }
}
