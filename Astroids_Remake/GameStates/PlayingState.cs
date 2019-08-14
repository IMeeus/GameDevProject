using Astroids_Remake.Components.Entities;
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
    /// Describes the behaviour of the game in its playing state.
    /// </summary>
    public class PlayingState : GameState
    {
        // Entities
        private EntityManager _entityManager;
        private Player _player;

        public PlayingState(IGame game) : base(game) { }

        public override void Initialize()
        {
            _entityManager = new EntityManager();
        }

        public override void LoadContent()
        {
            _game.Content.Unload();

            _player = new Player(_game.Content.Load<Texture2D>("player"), _game.Input)
            {
                Position = new Vector2(_game.ScreenWidth / 2, _game.ScreenHeight / 2)
            };

            _entityManager.AddEntity(_player);
        }

        public override void Update(float deltaTime)
        {
            _entityManager.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            _entityManager.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
