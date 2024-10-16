﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class EnemyManager : GameObject
    {
        private GraphicsManager graphicsManager;

        public EnemyManager(
            GraphicsManager graphicsManager,
            Rectangle boundary,
            int speed,
            Texture2D sprite,
            int bottomY,
            int topY,
            int fallSpeed,
            Vector2 pos
        ) : base(boundary, speed, sprite, bottomY, topY, fallSpeed, pos)
        {
            this.graphicsManager = graphicsManager;

        }

    }
}