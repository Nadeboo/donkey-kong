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
            this.Gravity = 100f;
            this.MaxFallSpeed = 100f;
            this.spriteEffect = SpriteEffects.None;
            this.collisionManager = collisionManager;
        }

        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            base.Update(gameTime, collisionManager);
        }
        public  void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
    }

}