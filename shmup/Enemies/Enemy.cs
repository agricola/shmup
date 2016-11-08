using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shmup.Enemies
{
    class Enemy : Character
    {
        // list of tuples to represent movement: Vector2 is direction, int is delay (aka how long it moves for)
        //private List<Tuple<Vector2, int>> movement = new List<Tuple<Vector2, int>>();

        // last time movement changed, used to keep track of time
        private double previousMoveTime = 0;

        // map bounds
        private Vector2 mapDimensions;

        // queue of actions for the enemy to perform
        private List<EnemyAction> actionQueue;

        // checks if it is a new Action so it doesnt spam bullets during fire action
        private EnemyAction previousAction = null;

        private bool exists = true;

        public bool Exists
        {
            get
            {
                return exists;
            }
        }

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
        }

        public void Update(GameTime gameTime)
        {
            // move enemy
            double totalMs = gameTime.TotalGameTime.TotalMilliseconds;
            if (totalMs - previousMoveTime > actionQueue[0].Delay && actionQueue.Count > 1)
            {
                actionQueue.RemoveAt(0);
                previousMoveTime = totalMs;
            }
            EnemyAction currentAction = actionQueue[0];
            Tuple<Vector2, bool> actionTuple = currentAction.Execute(position);
            position = actionTuple.Item1;
            bool shoot = actionTuple.Item2;
            if (shoot && currentAction != previousAction)
            {
                FireBullet();
            }
            previousAction = currentAction;

            // remove enemy if they exit screen
            exists = (position.X < 0 || position.X > mapDimensions.X || position.Y < 0 || position.Y > mapDimensions.Y) ? false : true;
        }

        public void FireBullet()
        {
            bulletManager.EnemyFireBullet(position);
        }
    }
}
