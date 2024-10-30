using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class PaulineManager : ObjectManager
    {
        public PaulineManager(GraphicsManager graphicsManager, int screenWidth)
            : base(new Rectangle(0, 0, 50, 50), graphicsManager.pauline, Vector2.Zero)
        {
            Position = new Vector2(screenWidth / 2f - graphicsManager.mario.Width / 2f, 10);
            UpdateBoundary();
        }

        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            base.Update(gameTime, collisionManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}