using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace donkey_kong
{
    public class GraphicsManager
    {
        private ContentManager content;
        public Texture2D floorTile;
        Texture2D wallTile;
        Texture2D mario;
        public GraphicsManager(ContentManager content)
        {
            this.content = content;
        }
        public void loadContent()
        {
            floorTile = content.Load<Texture2D>("floortile");
            wallTile = content.Load<Texture2D>("wallTile");
            mario = content.Load<Texture2D>("SuperMarioFront");
        }
        public void Update()
        {

        }
        public void drawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mario, Vector2.Zero, null, Color.Black);
        }
        public void DrawFloor(SpriteBatch spriteBatch)
        {

        }
        public void DrawWalls(SpriteBatch spriteBatch, SpriteFont font, string text, List<string> strings)
        {
            spriteBatch.DrawString(font, text, new Vector2(100,100), Color.Black);
            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == '1')
                    {
                        spriteBatch.Draw(floorTile, new Vector2(50 * j, 50 * i), Color.Green);
                    }
                }
            }
        }
    }

}
