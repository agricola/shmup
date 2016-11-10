using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    abstract class CollidableGameObject : GameObject
    {
        // ratio of the size of the texture by collider size
        protected float colliderRatio;
        public int ColliderHeight
        {
            get
            {
                return (int)(texture.Height * colliderRatio);
            }
        }
        public int ColliderWidth
        {
            get
            {
                return (int)(texture.Width * colliderRatio);
            }
        }
        public Vector2 ColliderPosition
        {
            get
            {
                return position + new Vector2(Width / 2, Height / 2) - new Vector2(ColliderWidth, ColliderHeight);
            }
        }
    }
}
