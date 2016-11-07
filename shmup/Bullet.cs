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
    class Bullet
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 direction;
        private int movementSpeed;
        private Vector2 mapDimensions;
        private bool exists;
        private bool goodSide;

        public bool Exists
        {
            get { return exists; }
        }

        public void Initialize(Texture2D texture, Vector2 position, Vector2 direction, int movementSpeed, bool goodSide, Vector2 mapDimensions)
        {
            this.texture = texture;
            this.position = position;
            direction.Normalize();
            this.direction = direction;
            this.movementSpeed = movementSpeed;
            this.mapDimensions = mapDimensions;
            this.goodSide = goodSide;
            exists = true;
        }

        public void Update(GameTime gameTime)
        {
            position += direction * movementSpeed;
            exists = (position.X < 0 || position.X > mapDimensions.X || position.Y < 0 || position.Y > mapDimensions.Y) ? false : true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
