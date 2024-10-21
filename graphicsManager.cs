using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace donkey_kong
{
    public class GraphicsManager
    {
        private ContentManager content;
        public Texture2D floorTile { get; private set; }
        public Texture2D wallTile { get; private set; }
        public Texture2D mario { get; private set; }
        public Texture2D pauline { get; private set; }

        public GraphicsManager(ContentManager content)
        {
            this.content = content;
        }

        public void LoadContent()
        {
            floorTile = content.Load<Texture2D>("floortile");
            wallTile = content.Load<Texture2D>("wallTile");
            mario = content.Load<Texture2D>("mario");
            pauline = content.Load<Texture2D>("pauline");
        }

        public void DrawWalls(SpriteBatch spriteBatch, SpriteFont font, string text, List<string> strings)
        {
            spriteBatch.DrawString(font, text, new Vector2(100, 100), Color.White);

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    Vector2 position = new Vector2(50 * j, 50 * i);
                    if (strings[i][j] == '1')
                    {
                        spriteBatch.Draw(floorTile, position, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(wallTile, position, Color.White);
                    }
                }
            }
        }

        public void DrawFloor(SpriteBatch spriteBatch)
        {
        }

        public void Update()
        {
        }
    }
}
