using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace donkey_kong
{
    public class ObjectManager
    {
        public Rectangle Boundary { get; protected set; }
        public Texture2D Sprite { get; protected set; }
        public Vector2 Position { get; protected set; }
        protected Vector2 velocity;
        protected float Gravity = 980f;
        protected float MaxFallSpeed = 2000f;
        protected bool isOnGround = false;

        public ObjectManager(Rectangle boundary, Texture2D sprite, Vector2 position)
        {
            Boundary = boundary;
            Sprite = sprite;
            Position = position;
            velocity = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime, CollisionManager collisionManager)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            ApplyGravity(deltaTime);
            UpdatePosition(deltaTime, collisionManager);
            UpdateBoundary();
        }

        protected virtual void ApplyGravity(float deltaTime)
        {
            if (!isOnGround)
            {
                velocity.Y += Gravity * deltaTime;
                velocity.Y = Math.Min(velocity.Y, MaxFallSpeed);
            }
        }

        protected virtual void UpdatePosition(float deltaTime, CollisionManager collisionManager)
        {
            Vector2 nextPosition = Position + velocity * deltaTime;
            Rectangle nextBounds = new Rectangle((int)nextPosition.X, (int)nextPosition.Y, Boundary.Width, Boundary.Height);

            if (!collisionManager.CheckCollision(nextBounds))
            {
                Position = nextPosition;
                isOnGround = false;
            }
            else
            {
                HandleCollision(collisionManager);
            }
        }

        protected virtual void HandleCollision(CollisionManager collisionManager)
        {
            if (velocity.Y > 0)
            {
                isOnGround = true;
                velocity.Y = 0;
            }
            else if (velocity.Y < 0)
            {
                velocity.Y = 0;
            }

            if (velocity.X != 0)
            {
                velocity.X = 0;
            }
        }

        protected void UpdateBoundary()
        {
            Boundary = new Rectangle((int)Position.X, (int)Position.Y, Boundary.Width, Boundary.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}