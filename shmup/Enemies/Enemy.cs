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
        private double? previousMoveTime = null;

        // queue of actions for the enemy to perform
        private List<EnemyAction> actionQueue;

        // checks if it is a new Action so it doesnt spam bullets during fire action
        private EnemyAction previousAction = null;

        // allows the enemy to come from off screen
        private bool isActive = false;

        public void Initialize(
            Texture2D texture,
            Vector2 startPosition,
            BulletManager bulletManager,
            Vector2 mapDimensions,
            List<EnemyAction> actionQueue)
        {
            this.texture = texture;
            position = startPosition;
            this.bulletManager = bulletManager;
            this.mapDimensions = mapDimensions;
            this.actionQueue = actionQueue;
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
            if (previousMoveTime == null)
            {
                previousMoveTime = totalMs;
            }
            EnemyAction currentAction = actionQueue[0];
            Tuple<Vector2, bool> actionTuple = currentAction.Execute(position);
            position = actionTuple.Item1;

            // fire bullets
            bool shoot = actionTuple.Item2;
            if (shoot && currentAction != previousAction)
            {
                FireBullet();
            }
            // move enemy
            if (totalMs - previousMoveTime > actionQueue[0].Delay && actionQueue.Count > 1)
            {
                actionQueue.RemoveAt(0);
                previousMoveTime = totalMs;
            }
            previousAction = currentAction;
        }

        public void FireBullet()
        {
            bulletManager.EnemyFireBullet(this);
        }
    }
}
