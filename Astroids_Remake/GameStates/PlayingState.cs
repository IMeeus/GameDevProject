using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Components.Entities.Player;
using Astroids_Remake.Components.Levels;
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
        private IMeteorFactory _meteorFactory;
        private Player _player;

        // Levels
        private ILevelManager _levelManager;
        private ILevelFactory _levelFactory;

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
            LoadLevels();
        }

        /// <summary>
        /// Initialiseert de properties van de statische klasse TextureHolder, zodat alle textures overal bereikbaar worden.
        /// </summary>
        private void LoadTextures()
        {
            TextureHolder.PlayerTexture = _game.Content.Load<Texture2D>("player");
            TextureHolder.LaserTextures = new Dictionary<string, Texture2D>()
            {
                {
                    "light", _game.Content.Load<Texture2D>("bullet_light")
                },
                {
                    "medium", _game.Content.Load<Texture2D>("bullet_light")
                },
                {
                    "strong", _game.Content.Load<Texture2D>("bullet_light")
                }
            };

            TextureHolder.MeteorTextures = new Dictionary<string, Texture2D>()
            {
                {
                    "tiny", _game.Content.Load<Texture2D>("meteor_tiny")
                },
                {
                    "small", _game.Content.Load<Texture2D>("meteor_small")
                },
                {
                    "medium", _game.Content.Load<Texture2D>("meteor_med")
                },
                {
                    "big", _game.Content.Load<Texture2D>("meteor_big")
                }
            };

            TextureHolder.BackgroundTextures = new Dictionary<string, Texture2D>()
            {
                {
                    "planet_blue", _game.Content.Load<Texture2D>("planet_blue")
                },
                {
                    "planet_brown", _game.Content.Load<Texture2D>("planet_brown")
                },
                {
                    "planet_red", _game.Content.Load<Texture2D>("planet_red")
                }
            };
        }

        /// <summary>
        /// Loads all levels into the level manager.
        /// </summary>
        private void LoadLevels()
        {
            _levelManager = new LevelManager();
            _meteorFactory = new MeteorFactory(_entityManager, _game);
            _levelFactory = new MeteorLevelFactory(_entityManager, _meteorFactory, _game);
            _levelManager.EnqueueLevel(_levelFactory.CreateLevel(LevelDifficulty.EASY));
            _levelManager.EnqueueLevel(_levelFactory.CreateLevel(LevelDifficulty.NORMAL));
            _levelManager.EnqueueLevel(_levelFactory.CreateLevel(LevelDifficulty.HARD));
        }

        /// <summary>
        /// Adds a player entity to the entity manager
        /// </summary>
        private void LoadPlayer()
        {
            _player = new Player(TextureHolder.PlayerTexture, _game.Input, new LaserFactory(_entityManager))
            {
                Position = _game.Center
            };

            _entityManager.AddEntity(_player);
        }

        public override void Update(float deltaTime)
        {
            _entityManager.Update(deltaTime);
            _levelManager.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
            _entityManager.Draw(spriteBatch);
            _levelManager.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
