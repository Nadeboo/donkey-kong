using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class gameObject
    {
        public Rectangle Boundary { get; set; }
        public int Speed { get; set; }
        public Texture2D Sprite { get; set; }
        public int BottomY { get; set; }
        public int TopY { get; set; }
        public int FallSpeed { get; set; }
        public Vector2 Pos { get; set; }

        public gameObject(Rectangle boundary, int speed, Texture2D sprite, int bottomY, int topY, int fallSpeed, Vector2 pos)
        {
            Boundary = boundary;
            Speed = speed;
            Sprite = sprite;
            BottomY = bottomY;
            TopY = topY;
            FallSpeed = fallSpeed;
            Pos = pos;
        }
    }
}
