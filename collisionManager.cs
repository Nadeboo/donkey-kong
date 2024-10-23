using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace donkey_kong
{
    public class CollisionManager
    {
        private List<Rectangle> collisionTiles;
        private List<Rectangle> ladderTiles;

        public CollisionManager()
        {
            collisionTiles = new List<Rectangle>();
            ladderTiles = new List<Rectangle>();
        }

        public void UpdateCollisionTiles(List<string> levelData)
        {
            collisionTiles.Clear();
            ladderTiles.Clear();

            for (int i = 0; i < levelData.Count; i++)
            {
                for (int j = 0; j < levelData[i].Length; j++)
                {
                    switch (levelData[i][j])
                    {
                        case 'F':
                            collisionTiles.Add(new Rectangle(50 * j, 50 * i, 50, 50));
                            break;
                        case '=':
                            ladderTiles.Add(new Rectangle(50 * j, 50 * i, 50, 50));
                            break;
                    }
                }
            }
        }

        public bool CheckCollision(Rectangle objectBounds)
        {
            foreach (Rectangle tileRect in collisionTiles)
            {
                if (objectBounds.Intersects(tileRect))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOnLadder(Rectangle objectBounds)
        {
            foreach (Rectangle ladder in ladderTiles)
            {
                if (objectBounds.Intersects(ladder))
                {
                    return true;
                }
            }
            return false;
        }
    }
}