using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace donkey_kong
{
    public class PlayerManager : ObjectManager
    {
        private const float JumpForce = -100f;
        private const float MoveSpeed = 50f;
        private float jumpTimer;
        private const float JumpDuration = 0.3f;
        private SpriteEffects spriteEffect;

        public PlayerManager(
            Rectangle boundary,
            Texture2D sprite,
            Vector2 position
        ) : base(boundary, sprite, position)
        {
            this.Gravity = 100f;
            this.MaxFallSpeed = 100f;
            this.spriteEffect = SpriteEffects.None;
        }

        public override void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            HandleInput(deltaTime);
            base.Update(gameTime, collisionManager);
        }

        private void HandleInput(float deltaTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X = -MoveSpeed;
                spriteEffect = SpriteEffects.FlipHorizontally; 
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X = MoveSpeed;
                spriteEffect = SpriteEffects.None; 
            }
            else
            {
                velocity.X = 0;
            }

            if (KeyboardManager.HasBeenPressed(Keys.Up) && isOnGround)
            {
                velocity.Y = JumpForce;
                jumpTimer = JumpDuration;
                isOnGround = false;
            }

            if (jumpTimer > 0)
            {
                jumpTimer -= deltaTime;
                if (jumpTimer <= 0 || !keyboardState.IsKeyDown(Keys.Up))
                {
                    jumpTimer = 0;
                    if (velocity.Y < 0)
                        velocity.Y *= 0.5f;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
    }
}