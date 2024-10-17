using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace donkey_kong
{
    public class PlayerManager : GameObject 
    {
        private GraphicsManager graphicsManager;
        private CollisionManager collisionManager;
        private bool isJumping;

        public PlayerManager(
            GraphicsManager graphicsManager,
            CollisionManager collisionManager,
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
            this.collisionManager = collisionManager;


        }

        public void move (GameTime gameTime)
        {
            x = Pos.X;
            y = Pos.Y;

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                x -= 5;
            }
            if (KeyboardManager.HasBeenPressed(Keys.Up) && isJumping == false)
            {
                isJumping = true;
                y -= 45;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                isJumping = false;
                x += 5;
            }

            if (collisionManager.CheckCollision(this))
            {
                isJumping = false;
                y -= 1;
            }
            else
            {
                y += 1;
            }
            Pos = new Vector2(x, y);
        }

        public void drawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(graphicsManager.mario, new Vector2(x, y), null, Color.White);
        }

    }
}
