using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    abstract class Character
    {
        protected Texture2D texture;
        protected int movementSpeed = 5;
        protected Vector2 position;
        protected BulletManager bulletManager;
        protected int health = 100;
        protected bool isGood = false;
        protected bool exists = true;

        public bool IsGood
        {
            get
            {
                return isGood;
            }
        }
        public bool Exists
        {
            get
            {
                return exists;
            }
        }
        public int Height
        {
            get { return texture.Height; }
        }
        public int Width
        {
            get { return texture.Width; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

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
