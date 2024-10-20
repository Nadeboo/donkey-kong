using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class EnemyManager : GameObject
    {
        private GraphicsManager graphicsManager;
        private int speed;
        private int bottomY;
        private int topY;
        private int fallSpeed;

        public EnemyManager(
            GraphicsManager graphicsManager,
            Rectangle boundary,
            int speed,
            Texture2D sprite,
            int bottomY,
            int topY,
            int fallSpeed,
            Vector2 pos
        ) : base(boundary, sprite, pos)
        {
            this.graphicsManager = graphicsManager;
            this.speed = speed;
            this.bottomY = bottomY;
            this.topY = topY;
            this.fallSpeed = fallSpeed;
        }

        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            base.Update(gameTime, collisionManager);
        }
    }
}