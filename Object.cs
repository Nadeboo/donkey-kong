using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class GameObject
    {
        public Rectangle Boundary { get; set; }
        public float Speed { get; set; }
        public Texture2D Sprite { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float FallSpeed { get; set; }
        public Vector2 Pos { get; set; }

        public GameObject(Rectangle boundary, int speed, Texture2D sprite, float x, float y, int fallSpeed, Vector2 pos)
        {
            Boundary = boundary;
            Speed = speed;
            Sprite = sprite;
            x = Pos.X;
            y = Pos.Y;
            FallSpeed = fallSpeed;
            Pos = pos;
        }
    }
}
