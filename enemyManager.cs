using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class EnemyManager : gameObject
    {
        public EnemyManager() : base(
            new Rectangle(0, 0, 0, 0),
            0,
            null,
            0,
            0,
            0,
            Vector2.Zero
        )
        {

        }
    }
}