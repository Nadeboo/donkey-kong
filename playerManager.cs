using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace donkey_kong
{
    public class PlayerManager : ObjectManager
    {
        private const float JumpForce = -100f;
        private const float MoveDelay = 0.5f; // Half second delay between moves
        private const int GridSize = 50; // Size of each movement step
        private float moveTimer = 0f; // Timer to track movement delay
        private float jumpTimer;
        private const float JumpDuration = 0.3f;
        private SpriteEffects spriteEffect;
        private CollisionManager collisionManager;

        public PlayerManager(
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
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update move timer
            if (moveTimer > 0)
            {
                moveTimer -= deltaTime;
            }

            HandleInput(deltaTime);
            base.Update(gameTime, collisionManager);
        }

        private void HandleInput(float deltaTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Only process movement if the timer has expired
            if (moveTimer <= 0)
            {
                Vector2 targetPosition = Position;
                bool shouldMove = false;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    targetPosition.X -= GridSize;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    shouldMove = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    targetPosition.X += GridSize;
                    spriteEffect = SpriteEffects.None;
                    shouldMove = true;
                }

                if (keyboardState.IsKeyDown(Keys.Up) && collisionManager.IsOnLadder(Boundary))
                {
                    targetPosition.Y -= GridSize;
                    shouldMove = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Down) && collisionManager.IsOnLadder(Boundary))
                {
                    targetPosition.Y += GridSize;
                    shouldMove = true;
                }

                if (shouldMove)
                {
                    TryMove(targetPosition);
                }
            }

            // Handle jumping
            if (KeyboardManager.HasBeenPressed(Keys.Space) && isOnGround)
            {
                velocity.Y = JumpForce;
                jumpTimer = JumpDuration;
                isOnGround = false;
            }

            if (jumpTimer > 0)
            {
                jumpTimer -= deltaTime;
                if (jumpTimer <= 0 || !keyboardState.IsKeyDown(Keys.Space))
                {
                    jumpTimer = 0;
                    if (velocity.Y < 0)
                        velocity.Y *= 0.5f;
                }
            }
        }

        private void TryMove(Vector2 targetPosition)
        {
            // Create a rectangle for the proposed position
            Rectangle proposedBounds = new Rectangle(
                (int)targetPosition.X,
                (int)targetPosition.Y,
                Boundary.Width,
                Boundary.Height
            );

            // Check if the move would result in a collision
            if (!collisionManager.CheckCollision(proposedBounds))
            {
                // If no collision, perform the move and reset the timer
                Position = targetPosition;
                moveTimer = MoveDelay;

                // Update the boundary after moving
                UpdateBoundary();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
    }
}