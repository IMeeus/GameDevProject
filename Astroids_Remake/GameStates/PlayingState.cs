using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Laser;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Components.Entities.Player;
using Astroids_Remake.Components.Levels;
using Astroids_Remake.Extra;
using Astroids_Remake.Graphicals;
using Astroids_Remake.Graphicals.Overlay;
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
    public class PlayingState : GameState, IGameScore
    {
        private SpriteFont _font;

        // Entities
        private IEntityManager _entityManager;
        private IMeteorFactory _meteorFactory;
        private Player _player;

        // Levels
        private ILevelManager _levelManager;
        private ILevelFactory _levelFactory;

        // Systems
        private ISystemManager _systemManager;

        // Overlays
        private Healthbar _healthbar;
        private TextField _scoreBoard;

        public int Score { get; set; }

        public PlayingState(IGame game) : base(game) { }

        public override void Initialize()
        {
            Score = 0;
            _entityManager = new EntityManager();
        }

        public override void LoadContent()
        {
            _game.Content.Unload();

            _font = _game.Content.Load<SpriteFont>("font");

            LoadTextures();
            LoadPlayer();
            LoadLevels();
            LoadSystems();
            LoadOverlays();
        }

        /// <summary>
        /// Loads in all textures for this state.
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
            TextureHolder.AddTexture("healthbar", _game.Content.Load<Texture2D>("healthbar"));
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
        /// Loads all game mechanics.
        /// </summary>
        private void LoadSystems()
        {
            _systemManager = new SystemManager();
            _systemManager.AddSystem(new BorderSystem(_entityManager, _game));
            _systemManager.AddSystem(new CollisionSystem(_entityManager, _meteorFactory, this));
        }

        private void LoadOverlays()
        {
            _healthbar = new Healthbar(TextureHolder.Textures["healthbar"], Color.FromNonPremultiplied(255, 255, 255, 100), new Rectangle(20, 20, _game.ScreenWidth - 40, 20), _player);
            _scoreBoard = new TextField(_game.Content.Load<SpriteFont>("font"), new Vector2(20, 50), 2f, Color.FromNonPremultiplied(255, 255, 255, 100));
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
            if (_player.IsDestroyed)
                _game.SetState(new ScoreState(_game, Score));

            _entityManager.Update(deltaTime);
            _levelManager.Update(deltaTime);
            _systemManager.Update(deltaTime);
            _healthbar.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
            _entityManager.Draw(spriteBatch);
            _levelManager.Draw(spriteBatch);
            _healthbar.Draw(spriteBatch);
            _scoreBoard.Draw(spriteBatch, "Score: " + Score, false);
            spriteBatch.End();
        }
    }
}