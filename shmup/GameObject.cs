using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    abstract class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 direction;
        protected Vector2 mapDimensions;
        protected float scale;
        protected bool exists;
        protected bool isGood;

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
            get
            {
                return (int)(texture.Height * scale);
            }
        }
        public int Width
        {
            get
            {
                return (int)(texture.Width * scale);
            }
        }
        public Vector2 CenterPosition
        {
            get
            {
                return Vector2.Add(Position, new Vector2(Width / 2, Height / 2));
            }
        }
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
