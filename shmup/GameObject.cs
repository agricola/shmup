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
                return texture.Height;
            }
        }
        public int Width
        {
            get
            {
                return texture.Width;
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
    }
}
