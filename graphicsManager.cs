using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace donkey_kong
{
    public class GraphicsManager
    {
        private ContentManager content;
        public Texture2D floorTile;
        Texture2D wallTile;
        public GraphicsManager(ContentManager content)
        {
            this.content = content;
        }
        public void loadContent()
        {
            floorTile = content.Load<Texture2D>("floortile");
            wallTile = content.Load<Texture2D>("wallTile");
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }

}
