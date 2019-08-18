using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Components.Entities.Player;
using Astroids_Remake.Components.Levels;
using Astroids_Remake.Extra;
using Astroids_Remake.Systems;
using Astroids_Remake.Tools;
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

        // Systems
        private ISystemManager _systemManager;

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
            LoadSystems();
        }

        /// <summary>
        /// Initialiseert de properties van de statische klasse TextureHolder, zodat alle textures overal bereikbaar worden.
        /// </summary>
        private void LoadTextures()
        {
            TextureHolder.AddTexture("player", _game.Content.Load<Texture2D>("player"));
            TextureHolder.AddTexture("explosion", _game.Content.Load<Texture2D>("explosion1"));
            TextureHolder.AddTexture("bullet_light", _game.Content.Load<Texture2D>("bullet_light"));
            TextureHolder.AddTexture("bullet_medium", _game.Content.Load<Texture2D>("bullet_medium"));
            TextureHolder.AddTexture("bullet_heavy", _game.Content.Load<Texture2D>("bullet_heavy"));
            TextureHolder.AddTexture("meteor_tiny", _game.Content.Load<Texture2D>("meteor_tiny"));
            TextureHolder.AddTexture("meteor_small", _game.Content.Load<Texture2D>("meteor_small"));
            TextureHolder.AddTexture("meteor_medium", _game.Content.Load<Texture2D>("meteor_med"));
            TextureHolder.AddTexture("meteor_big", _game.Content.Load<Texture2D>("meteor_big"));
            TextureHolder.AddTexture("planet_blue", _game.Content.Load<Texture2D>("planet_blue"));
            TextureHolder.AddTexture("planet_brown", _game.Content.Load<Texture2D>("planet_brown"));
            TextureHolder.AddTexture("planet_red", _game.Content.Load<Texture2D>("planet_red"));
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

        private void LoadSystems()
        {
            _systemManager = new SystemManager();
            _systemManager.AddSystem(new BorderSystem(_entityManager, _game));
            _systemManager.AddSystem(new CollisionSystem(_entityManager, _meteorFactory));
        }

        /// <summary>
        /// Adds a player entity to the entity manager
        /// </summary>
        private void LoadPlayer()
        {
            _player = new Player(TextureHolder.Textures["player"], _game.Input, new LaserFactory(_entityManager))
            {
                Position = _game.Center
            };

            _entityManager.AddEntity(_player);
        }

        public override void Update(float deltaTime)
        {
            _entityManager.Update(deltaTime);
            _levelManager.Update(deltaTime);
            _systemManager.Update(deltaTime);
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