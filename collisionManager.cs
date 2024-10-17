using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace donkey_kong
{
    public class CollisionManager
    {
        private List<Rectangle> collisionTiles;

        public CollisionManager()
        {
            collisionTiles = new List<Rectangle>();
        }

        public void UpdateCollisionTiles(List<string> levelData)
        {
            collisionTiles.Clear();
            for (int i = 0; i < levelData.Count; i++)
            {
                for (int j = 0; j < levelData[i].Length; j++)
                {
                    if (levelData[i][j] == '1')
                    {
                        collisionTiles.Add(new Rectangle(50 * j, 50 * i, 50, 50));
                    }
                }
            }
        }

        public bool CheckCollision(PlayerManager player)
        {
            // Create a new rectangle with current position but same size as boundary
            bool playerIntersectsTiles = false;
            Rectangle currentBounds = new Rectangle(
                (int)player.x,
                (int)player.y,
                player.Boundary.Width,
                player.Boundary.Height
            );

            foreach (Rectangle tileRect in collisionTiles)
            {
                if (currentBounds.Intersects(tileRect))
                {
                    playerIntersectsTiles = true;
                    break;
                }
            }

            return playerIntersectsTiles;
        }
    }
}