using Astroids_Remake.Extra;
using Astroids_Remake.GameLogic.Input;
using Astroids_Remake.GameStates;
using Astroids_Remake.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Astroids_Remake
{
    public interface IGame: IGameDimensions, IGameStates
    {
        GraphicsDevice GraphicsDevice { get; }
        ContentManager Content { get; }
        Input Input { get; }

        void Exit();
    }

    public interface IGameDimensions
    {
        int ScreenWidth { get; }
        int ScreenHeight { get; }
        Vector2 Center { get; }
    }

    public interface IGameStates
    {
        void SetState(GameState newState);
    }

    public interface IGameScore
    {
        int Score { get; set; }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MyGame : Game, IGame
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public Vector2 Center => new Vector2(ScreenWidth / 2, ScreenHeight / 2);
        public Input Input { get; private set; }
        public GameState CurrentState { get; private set; }

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitializeScreenSize();
            InitializeGameStates();
            InitializeInput();

            base.Initialize();
        }


        /// <summary>
        /// Changes the size of the game to the size of the screen.
        /// </summary>
        private void InitializeScreenSize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.ApplyChanges();

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Initializes the GameState Properties
        /// </summary>
        private void InitializeGameStates()
        {
            CurrentState = new MainMenuState(this);
            CurrentState.Initialize();
        }

        /// <summary>
        /// Initializes the Input Properties
        /// </summary>
        private void InitializeInput()
        {
            Input = new Input(new AzertyControls());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            CurrentState.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            TextureHolder.Clear();
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.Update();
            CurrentState.Update((float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            CurrentState.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes the state of the game.
        /// </summary>
        /// <param name="newGameState">The new state of the game</param>
        public void SetState(GameState newState)
        {
            UnloadContent();

            CurrentState = newState;
            CurrentState.Initialize();
            CurrentState.LoadContent();
        }
    }
}
