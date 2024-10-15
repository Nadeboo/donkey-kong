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
        public string text;
        public bool start = false;
        public SpriteFont font;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphicsManager.loadContent();
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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (start)
            {
                foreach (var tile in tiles)
                {
                    tile.Update(); //tom funktion -> graphicsManager.cs
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                start = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
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
            graphicsManager.drawPlayer(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
