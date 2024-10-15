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
        private List<string> strings = new List<string>();

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
                    tile.Draw(spriteBatch); //tom funktion -> graphicsManager.cs
                }
            }
            else

            {
                spriteBatch.DrawString(font, text, new Vector2(100, 100), Color.Black);
                for (int i = 0; i < strings.Count; i++)
                {
                    for (int j = 0; j < strings[i].Length; j++)
                    {
                        if (strings[i][j] == '1')
                        {
                            spriteBatch.Draw(graphicsManager.floorTile, new Vector2(50 * j, 50 * i), Color.Green);
                        }
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
