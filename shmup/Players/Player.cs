using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup.Players
{
    class Player : Character
    {
        public void Initialize(Texture2D texture, Vector2 startPosition, BulletManager bulletManager, float scale, float colliderRatio)
        {

            this.texture = texture;
            position = startPosition;
            this.bulletManager = bulletManager;
            this.colliderRatio = colliderRatio;
            this.scale = scale;
            movementSpeed = 5;
            isGood = true;
            exists = true;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void MovePlayer(int xDistance, int yDistance)
        {
            position.X += xDistance * movementSpeed;
            position.Y += yDistance * movementSpeed;
        }

        public void FireBullet()
        {
            Vector2 direction = new Vector2(0, -1);
            bulletManager.FireBullet(this, direction);
        }
    }
}
