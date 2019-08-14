using Astroids_Remake.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Astroids_Remake
{
    public interface IGame: IGameSize, IGameStates
    {
        GraphicsDevice GraphicsDevice { get; }
    }

    public interface IGameSize
    {
        int ScreenWidth { get; }
        int ScreenHeight { get; }
    }

    public interface IGameStates
    {
        GameState CurrentState { get; }
        GameState MenuState { get; }
        GameState PlayingState { get; }
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
        public GameState CurrentState { get; private set; }
        public GameState MenuState { get; private set; }
        public GameState PlayingState { get; private set; }

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
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // TODO: Add your update logic here

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
        public void SetState(GameState newGameState)
        {
            CurrentState = newGameState;
            CurrentState.Initialize();
            CurrentState.LoadContent();
        }
    }
}
