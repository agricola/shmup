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
                return (int)(Height * colliderRatio);
            }
        }
        public int ColliderWidth
        {
            get
            {
                return (int)(Width * colliderRatio);
            }
        }
        public Vector2 ColliderPosition
        {
            get
            {
                return CenterPosition - new Vector2(ColliderWidth / 2, ColliderHeight / 2);
            }
        }
    }
}
