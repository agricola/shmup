using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shmup.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    class BulletManager
    {
        private Texture2D bulletTexture;
        private int bulletSpeed;
        private Vector2 mapDimensions;
        private bool goodSide;
        private List<Bullet> bullets = new List<Bullet>();
        private Player player;

        public void Initialize(Texture2D bulletTexture, int bulletSpeed, Vector2 mapDimensions, bool goodSide, Player player)
        {
            this.bulletTexture = bulletTexture;
            this.bulletSpeed = bulletSpeed;
            this.mapDimensions = mapDimensions;
            this.goodSide = goodSide;
            this.player = player;
            //List<Tuple<Vector2, int>> list = new List<Tuple<Vector2, int>>();
            //list.Add(new Tuple<Vector2, int>(mapDimensions, 5));
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);
                if (!bullets[i].Exists) bullets.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine(bullets.Count);
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void FireBullet(Character character, Vector2 direction)
        {
            Bullet bullet = new Bullet();
            Vector2 position = character.CenterPosition;
            bullet.Initialize(bulletTexture, position, direction, bulletSpeed, goodSide, mapDimensions, 0.5f, 1f);
            bullets.Add(bullet);
        }

        public void EnemyFireBullet(Character character)
        {
            Bullet bullet = new Bullet();
            Vector2 position = character.CenterPosition;
            Vector2 direction = player.Position - position;
            direction.Normalize();
            bullet.Initialize(bulletTexture, position, direction, bulletSpeed, !goodSide, mapDimensions, 1.0f, 0.75f);
            bullets.Add(bullet);
        }

        public void CheckCollisionWithBullets(Character character)
        {
            Rectangle characterCollider = new Rectangle((int)character.ColliderPosition.X, (int)character.ColliderPosition.Y, character.ColliderWidth, character.ColliderHeight);
            foreach (var bullet in bullets)
            {
                Rectangle bulletCollider = new Rectangle((int)bullet.ColliderPosition.X, (int)bullet.ColliderPosition.Y, bullet.ColliderWidth, bullet.ColliderHeight);
                if (bulletCollider.Intersects(characterCollider))
                {
                    if (bullet.IsGood != character.IsGood)
                    {
                        character.TakeDamage(bullet.Damage);
                        bullet.Destroy();
                    }
                }
            }
        }
    }
}
