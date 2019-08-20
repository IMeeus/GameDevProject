using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astroids_Remake.Graphicals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroids_Remake.GameStates
{
    public class ScoreState : GameState
    {
        private int _score;
        private SpriteFont _font;
        private TextField _instructions;
        private TextField _scoreboard;

        public ScoreState(IGame game, int score) : base(game)
        {
            _score = score;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            _font = _game.Content.Load<SpriteFont>("font");
            _scoreboard = new TextField(_font, new Vector2(_game.Center.X, _game.Center.Y - _game.ScreenHeight / 2), 3, Color.White);
            _instructions = new TextField(_font, new Vector2(_game.Center.X, _game.Center.Y - _game.ScreenHeight / 2), 3, Color.White);
        }

        public override void Update(float deltaTime)
        {
            if (_game.Input.EnterPress)
                _game.SetState("mainMenu");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _scoreboard.Draw("Your score was: " + _score, spriteBatch);
            _instructions.Draw("Press Enter to go back to main menu.", spriteBatch);
        }
    }
}
