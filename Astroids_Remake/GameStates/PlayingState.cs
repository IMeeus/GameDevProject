using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private IEntityManager _entityManager;
        private Player _player;

        public PlayingState(IGame game) : base(game) { }

        public override void Initialize()
        {
            _entityManager = new EntityManager();
        }

        public override void LoadContent()
        {
            _game.Content.Unload();

            LoadTextures();
            LoadPlayer();
        }

        /// <summary>
        /// Initialiseert de properties van de statische klasse TextureHolder, zodat alle textures overal bereikbaar worden.
        /// </summary>
        private void LoadTextures()
        {
            TextureHolder.PlayerTexture = _game.Content.Load<Texture2D>("player");
            Debug.WriteLine("STEP 1");
            TextureHolder.LaserTextures = new Dictionary<LaserType, Texture2D>()
            {
                {
                    LaserType.LIGHT, _game.Content.Load<Texture2D>("bullet_light")
                },
                {
                    LaserType.MEDIUM, _game.Content.Load<Texture2D>("bullet_light")
                },
                {
                    LaserType.STRONG, _game.Content.Load<Texture2D>("bullet_light")
                }
            };

            TextureHolder.MeteorTextures = new Dictionary<MeteorType, Texture2D>()
            {
                {
                    MeteorType.TINY, _game.Content.Load<Texture2D>("meteor_tiny")
                },
                {
                    MeteorType.SMALL, _game.Content.Load<Texture2D>("meteor_small")
                },
                {
                    MeteorType.MEDIUM, _game.Content.Load<Texture2D>("meteor_med")
                },
                {
                    MeteorType.BIG, _game.Content.Load<Texture2D>("meteor_big")
                }
            };
        }

        /// <summary>
        /// Voegt een speler toe aan de entity manager.
        /// </summary>
        private void LoadPlayer()
        {
            _player = new Player(TextureHolder.PlayerTexture, _game.Input, new LaserFactory(_entityManager))
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
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
            _entityManager.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
