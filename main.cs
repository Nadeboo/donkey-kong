using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace donkey_kong
{
    public class main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private GraphicsManager graphicsManager;
        private EnemyManager enemyManager;
        private PlayerManager playerManager;
        private CollisionManager collisionManager;
        public string text;
        public bool start = false;
        public SpriteFont font;
        private List<GraphicsManager> tiles;
        public List<string> strings = new List<string>();
        public enum GameState { Start, InGame, GameOver, GameWon }
        public GameState CurrentGameState;

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
            CurrentGameState = GameState.InGame;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphicsManager.LoadContent();
            tiles = new List<GraphicsManager>();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            StreamReader sr = new StreamReader("maze.txt");
            text = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            font = Content.Load<SpriteFont>("font");

            // Create the player
            Rectangle playerBoundary = new Rectangle(100, 100, 50, 50);
            Vector2 initialPosition = new Vector2(playerBoundary.X, playerBoundary.Y);

            playerManager = new PlayerManager(
                playerBoundary,
                graphicsManager.mario,
                initialPosition
            );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                start = true;
            }
            KeyboardManager.Update();
            switch (CurrentGameState)
            {
                case GameState.Start:
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
                    playerManager.Update(gameTime, collisionManager);
                    base.Update(gameTime);
                    break;

                case GameState.GameWon:
                    break;

                case GameState.GameOver:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case GameState.Start:
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
                    spriteBatch.End();
                    break;

                case GameState.GameWon:
                    break;

                case GameState.GameOver:
                    break;
            }
            base.Draw(gameTime);
        }
    }
}