using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spaceInvaders;
using System.Collections.Generic;
using System.IO;

namespace donkey_kong
{
    public class main : Game
    {
        // Graphics-related fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private GraphicsManager graphicsManager;
        public SpriteFont font;
        private Texture2D youwin;
        private Texture2D youlose;
        private Texture2D startTexture;

        // Game object managers
        private EnemyManager enemyManager;
        private PlayerManager playerManager;
        private PaulineManager paulineManager;
        private CollisionManager collisionManager;
        private StartButton startButton;

        // Game state and screen properties
        public enum GameState { Start, InGame, GameOver, GameWon }
        public GameState CurrentGameState;
        public int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        // Game data
        public string text;
        public bool start = false;
        private List<GraphicsManager> tiles;
        public List<string> strings = new List<string>();

        public main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphicsManager = new GraphicsManager(Content);
            collisionManager = new CollisionManager();
            CurrentGameState = GameState.Start;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphicsManager.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            paulineManager = new PaulineManager(graphicsManager, screenWidth);
            youwin = Content.Load<Texture2D>("youwin");
            youlose = Content.Load<Texture2D>("youlose");
            startTexture = Content.Load<Texture2D>("bender");

            tiles = new List<GraphicsManager>();
            StreamReader sr = new StreamReader("maze.txt");
            text = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            font = Content.Load<SpriteFont>("font");

            // Create the player
            Rectangle playerBoundary = new Rectangle((screenWidth / 2) - 25, screenHeight - 400, 50, 50);
            Vector2 initialPosition = new Vector2(playerBoundary.X, playerBoundary.Y);

            playerManager = new PlayerManager(
                playerBoundary,
                graphicsManager.mario,
                initialPosition
            );
            startButton = new StartButton(Content, screenWidth, screenHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                start = true;

            KeyboardManager.Update();
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            switch (CurrentGameState)
            {
                case GameState.Start:
                    startButton.Update(mouseState);
                    if (startButton.IsClicked())
                    {
                        CurrentGameState = GameState.InGame;
                    }
                    break;

                case GameState.InGame:
                    collisionManager.UpdateCollisionTiles(strings);

                    if (start)
                    {
                        foreach (var tile in tiles)
                        {
                            tile.Update();
                        }
                    }

                    if (playerManager.Boundary.Intersects(paulineManager.Boundary))
                    {
                        CurrentGameState = GameState.GameWon;
                    }

                    playerManager.Update(gameTime, collisionManager);
                    paulineManager.Update(gameTime, collisionManager);
                    base.Update(gameTime);
                    break;

                case GameState.GameWon:
                    if (keyboardState.IsKeyDown(Keys.R))
                    {
                        CurrentGameState = GameState.InGame;
                    }
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case GameState.Start:
                    spriteBatch.Begin();
                    spriteBatch.Draw(startTexture, Vector2.Zero, Color.White);
                    startButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Begin();
                    if (start)
                    {
                        foreach (var tile in tiles)
                        {
                            tile.DrawFloor(spriteBatch);
                        }
                    }
                    else
                    {
                        graphicsManager.DrawWalls(spriteBatch, font, text, strings);
                    }
                    playerManager.Draw(spriteBatch);
                    paulineManager.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.GameWon:
                    spriteBatch.Draw(youwin, Vector2.Zero, Color.White);
                    break;

                case GameState.GameOver:
                    spriteBatch.Draw(youlose, Vector2.Zero, Color.White);
                    break;
            }
            base.Draw(gameTime);
        }
    }
}