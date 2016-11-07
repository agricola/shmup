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
        private List<Tuple<Vector2, int>> movement = new List<Tuple<Vector2, int>>();

        // last time movement changed, used to keep track of time
        private double lastMoveTime;

        // map bounds
        private Vector2 mapDimensions;

        private bool exists;

        public bool Exists
        {
            get { return exists; }
        }

        public void Initialize(Texture2D texture, Vector2 startPosition, BulletManager bulletManager, Vector2 mapDimensions, List<Tuple<Vector2, int>> movement)
        {
            this.texture = texture;
            position = startPosition;
            this.bulletManager = bulletManager;
            this.movement = movement;
            this.mapDimensions = mapDimensions;
            lastMoveTime = 0;
            exists = true;
        }

        public void Update(GameTime gameTime)
        {
            // move enemy
            double totalMs = gameTime.TotalGameTime.TotalMilliseconds;
            if (totalMs - lastMoveTime > movement[0].Item2 && movement.Count > 1)
            {
                movement.RemoveAt(0);
                lastMoveTime = totalMs;
            }
            position += movement[0].Item1;

            // remove enemy if they exit screen
            exists = (position.X < 0 || position.X > mapDimensions.X || position.Y < 0 || position.Y > mapDimensions.Y) ? false : true;
        }
    }
}
