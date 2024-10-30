using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class EnemyManager : ObjectManager
    {
        private GraphicsManager graphicsManager;
        private float speed;
        private const float MIN_SPEED = 100f;  
        private const float MAX_SPEED = 200f;
        private int direction = 1;  
        private SpriteEffects spriteEffect;
        private CollisionManager collisionManager;

        public EnemyManager(
            Rectangle boundary,
            Texture2D sprite,
            Vector2 position,
            CollisionManager collisionManager
        ) : base(boundary, sprite, position)
        {
            this.Gravity = 0f; 
            this.MaxFallSpeed = 0f;
            this.spriteEffect = SpriteEffects.None;
            this.collisionManager = collisionManager;

            Random random = new Random();
            this.speed = random.Next(100, 200);
        }

        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float newX = Position.X + (speed * direction * deltaTime);

            if (newX <= 0)
            {
                newX = 0;
                direction *= -1;  
                spriteEffect = SpriteEffects.None;
            }
            else if (newX >= 800 - Boundary.Width) 
            {
                newX = 800 - Boundary.Width;
                direction *= -1; 
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            Position = new Vector2(newX, Position.Y);

            Boundary = new Rectangle((int)Position.X, (int)Position.Y, Boundary.Width, Boundary.Height);

            base.Update(gameTime, collisionManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
            {
                spriteBatch.Draw(
                    Sprite,
                    new Rectangle((int)Position.X, (int)Position.Y, 76, 40),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    spriteEffect,
                    0
                );
            }
        }
    }
}