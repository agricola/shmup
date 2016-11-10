using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace shmup.Enemies
{
    class Enemy : Character
    {
        // last time movement changed, used to keep track of time
        private double? previousShootTime = null;

        // allows the enemy to come from off screen
        private bool isActive = false;

        private List<Vector2> moveQueue;

        // delay and bullet type (possibly make this its own type)
        private List<int> shootQueue;

        public void Initialize(
            Texture2D texture,
            Vector2 startPosition,
            BulletManager bulletManager,
            Vector2 mapDimensions,
            List<Vector2> moveQueue,
            List<int> shootQueue)
        {
            this.texture = texture;
            position = startPosition;
            this.bulletManager = bulletManager;
            this.mapDimensions = mapDimensions;
            this.moveQueue = moveQueue;
            this.shootQueue = shootQueue;
            colliderRatio = 0.5f;
            scale = 1.0f;
            isGood = false;
            exists = true;
        }

        public void Update(GameTime gameTime)
        {
            HandleActionQueue(gameTime);

            if (isActive)
            {
                // remove enemy if they exit screen
                exists = IsOnScreen();
            }
            else
            {
                // activate enemy when they enter screen
                isActive = IsOnScreen();
            }
        }

        private bool IsOnScreen()
        {
            return (position.X < -Width || position.X > mapDimensions.X || position.Y < -Height || position.Y > mapDimensions.Y) ? false : true;
        }

        private void HandleActionQueue(GameTime gameTime)
        {
            double totalMs = gameTime.TotalGameTime.TotalMilliseconds;
            if (previousShootTime == null)
            {
                previousShootTime = totalMs;
            }

            if (shootQueue.Count > 0 && totalMs - previousShootTime > shootQueue[0])
            {
                FireBullet();
                shootQueue.RemoveAt(0);
                previousShootTime = totalMs;
            }
            
            if (moveQueue.Count > 0 && Vector2.Subtract(moveQueue[0], position).Length() < movementSpeed)
            {
                moveQueue.RemoveAt(0);
            }
            Debug.WriteLine(Vector2.Subtract( moveQueue[0], position).Length());
            Vector2 direction = Vector2.Normalize(moveQueue[0] - position);
            position += direction * movementSpeed;
        }

        public void FireBullet()
        {
            bulletManager.EnemyFireBullet(this, movementSpeed + 1);
        }
    }
}
