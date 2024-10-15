﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class PlayerManager : GameObject 
    {
        private GraphicsManager graphicsManager;

        public PlayerManager(
            GraphicsManager graphicsManager,
            Rectangle boundary,
            int speed,
            Texture2D sprite,
            float x,
            float y,
            int fallSpeed,
            Vector2 pos
        ) : base(boundary, speed, sprite, x, y, fallSpeed, pos)
        {
            this.graphicsManager = graphicsManager;


        }
        public void drawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(graphicsManager.mario, new Vector2(x, y), null, Color.Black);
        }

    }
}
