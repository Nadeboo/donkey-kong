using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class EnemyManager : ObjectManager
    {
        private GraphicsManager graphicsManager;
        private int speed;
        private int fallSpeed;
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
        }

        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            base.Update(gameTime, collisionManager);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine($"Enemy Sprite null?: {Sprite == null}");
            //Console.WriteLine($"Position: {Position}");

            // Try the most basic possible draw call
            if (Sprite != null)
            {
                spriteBatch.Draw(
                    Sprite,
                    new Rectangle((int)Position.X, (int)Position.Y, 76, 40),
                    Color.White
                );
            }
        }
    }

}