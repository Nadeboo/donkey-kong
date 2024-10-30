using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spaceInvaders;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace donkey_kong
{
    public class main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private GraphicsManager graphicsManager;
        public SpriteFont font;
        private Texture2D youwin;
        private Texture2D youlose;
        private Texture2D startTexture;

        private List<EnemyManager> enemies; 
        private PlayerManager playerManager;
        private PaulineManager paulineManager;
        private CollisionManager collisionManager;
        private StartButton startButton;

        public enum GameState { Start, InGame, GameOver, GameWon }
        public GameState CurrentGameState;
        public int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public string text;
        public bool start = false;
        private List<GraphicsManager> tiles;
        public List<string> strings = new List<string>();
        private Vector2 marioPosition;
        private Vector2 paulinePosition;
        private Vector2 InitialMarioPosition;

        private int lives = 3;

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
            enemies = new List<EnemyManager>();  
            CurrentGameState = GameState.Start;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphicsManager.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            Vector2 marioPosition = FindCharacterPosition(strings, 'M');
            Vector2 paulinePosition = FindCharacterPosition(strings, 'P');
            InitialMarioPosition = marioPosition;

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == 'E')
                    {
                        Vector2 enemyPos = new Vector2(50 * j, 50 * i);
                        Rectangle enemyBoundary = new Rectangle(
                            (int)enemyPos.X,
                            (int)enemyPos.Y,
                            76, 
                            40   
                        );

                        enemies.Add(new EnemyManager(
                            enemyBoundary,
                            graphicsManager.enemy,
                            enemyPos,
                            collisionManager
                        ));
                    }
                }
            }

            font = Content.Load<SpriteFont>("font");

            Rectangle playerBoundary = new Rectangle(
                (int)marioPosition.X,
                (int)marioPosition.Y,
                50,
                50
            );

            playerManager = new PlayerManager(
                playerBoundary,
                graphicsManager.mario,
                marioPosition,
                collisionManager
            );

            paulineManager = new PaulineManager(graphicsManager, screenWidth)
            {
                Position = paulinePosition
            };

            startButton = new StartButton(Content, screenWidth, screenHeight);
        }

        private Vector2 FindCharacterPosition(List<string> mapData, char character)
        {
            for (int i = 0; i < mapData.Count; i++)
            {
                for (int j = 0; j < mapData[i].Length; j++)
                {
                    if (mapData[i][j] == character)
                    {
                        return new Vector2(50 * j, 50 * i);
                    }
                }
            }
            return Vector2.Zero;
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
                    if (keyboardState.IsKeyDown(Keys.R))
                    {
                        CurrentGameState = GameState.InGame;
                        playerManager.Position = InitialMarioPosition;
                        lives = 3;
                    }
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

                    foreach (var enemy in enemies)
                    {
                        enemy.Update(gameTime, collisionManager);

                        if (enemy.Boundary.Intersects(playerManager.Boundary))
                        {
                            lives--;
                            if (lives <= 0)
                            {
                                CurrentGameState = GameState.GameOver;
                                break;
                            }
                            else
                            {
                                playerManager.Position = InitialMarioPosition;
                                break;
                            }
                        }
                    }

                    playerManager.Update(gameTime, collisionManager);
                    paulineManager.Update(gameTime, collisionManager);
                    base.Update(gameTime);
                    break;

                case GameState.GameWon:
                case GameState.GameOver:
                    if (keyboardState.IsKeyDown(Keys.R))
                    {
                        CurrentGameState = GameState.InGame;
                        playerManager.Position = InitialMarioPosition;
                        lives = 3; 
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

                    foreach (var enemy in enemies)
                    {
                        enemy.Draw(spriteBatch);
                    }

                    playerManager.Draw(spriteBatch);
                    paulineManager.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Lives: " + lives, new Vector2(GraphicsDevice.Viewport.Width - 100, 10), Color.White);

                    spriteBatch.End();
                    break;

                case GameState.GameWon:
                    spriteBatch.Begin();
                    spriteBatch.Draw(youwin, Vector2.Zero, Color.White);
                    spriteBatch.End();
                    break;

                case GameState.GameOver:
                    spriteBatch.Begin();
                    spriteBatch.Draw(youlose, Vector2.Zero, Color.White);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }
    }
}