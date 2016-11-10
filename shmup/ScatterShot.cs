using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    class ScatterShot : Bullet
    {
        private const int Delay = 1000;
        private const int BulletCount = 12;

        private double? startTime = null;

        private List<Bullet> bullets = new List<Bullet>();

        private void Scatter()
        {
            for (int i = 0; i < BulletCount; i++)
            {
                Bullet bullet = new Bullet();
                float angle = (float)Math.PI * i / (BulletCount / 2);
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Debug.WriteLine(direction);
                bullet.Initialize(texture, position, direction, movementSpeed * 2, isGood, mapDimensions, scale, colliderRatio, recordBullets);
                bullets.Add(bullet);
            }
            recordBullets(bullets);
        }

        // TODO: restructure bullet classes in the future (w/ interface)
        public override void Update(GameTime gameTime)
        {
            double totalMs = gameTime.TotalGameTime.TotalMilliseconds;
            if (startTime == null)
            {
                startTime = totalMs;
            }
            if (totalMs - startTime > Delay)
            {
                Scatter();
                Destroy();
            }
            base.Update(gameTime);
        }
    }
}
