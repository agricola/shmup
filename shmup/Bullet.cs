using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    class Bullet : CollidableGameObject
    {
        private int movementSpeed;
        private int damage = 100;

        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public void Initialize(Texture2D texture, Vector2 position, Vector2 direction, int movementSpeed, bool isGood, Vector2 mapDimensions, float scale, float colliderRatio)
        {
            this.texture = texture;
            this.scale = scale;
            this.position = position;
            this.position = Vector2.Subtract(position, new Vector2(Width / 2, Height / 2));
            direction.Normalize();
            this.direction = direction;
            this.movementSpeed = movementSpeed;
            this.mapDimensions = mapDimensions;
            this.isGood = isGood;
            this.colliderRatio = colliderRatio;
            exists = true;
        }

        public void Update(GameTime gameTime)
        {
            position += direction * movementSpeed;

            // same line as in enemy
            if (exists)
            {
                exists = (position.X < -Width || position.X > mapDimensions.X || position.Y < -Height || position.Y > mapDimensions.Y) ? false : true;
            }
        }
    }
}
