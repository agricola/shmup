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
    abstract class Character : CollidableGameObject
    {
        protected int movementSpeed = 5;
        protected BulletManager bulletManager;
        protected int health = 100;

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                exists = false;
            }
        }
    }
}
