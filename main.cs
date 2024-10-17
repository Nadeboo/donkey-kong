using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

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
        Rectangle playerBoundary;
        int playerSpeed;
        float x;
        float y;
        int fallSpeed;
        Vector2 initialPosition;
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
            playerBoundary = new Rectangle(100, 100, 50, 50);
            playerSpeed = 5; 
            x = playerBoundary.X;
            y = playerBoundary.Y;
            fallSpeed = 2; 
            initialPosition = new Vector2(x, y);

            playerManager = new PlayerManager(
            graphicsManager,
            collisionManager,
            playerBoundary,
            playerSpeed,
            graphicsManager.mario,
            x,
            y,
            fallSpeed,
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
                            tile.Update(); //tom funktion -> graphicsManager.cs
                        }
                    }
                    playerManager.move(gameTime);
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
                    spriteBatch.Begin();
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    if (start)
                    {
                        foreach (var tile in tiles)
                        {
                            tile.DrawFloor(spriteBatch); //tom funktion -> graphicsManager.cs
                        }
                    }
                    else

                    {
                        graphicsManager.DrawWalls(spriteBatch, font, text, strings);
                    }
                    playerManager.drawPlayer(spriteBatch);
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
