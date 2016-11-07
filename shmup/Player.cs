using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    class Player
    {
        //private const float Scale = 1.0f;
        private Texture2D texture;
        private int movementSpeed = 5;
        private Vector2 position;

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


        public void Initialize(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void MovePlayer(int xDistance, int yDistance)
        {
            position.X += xDistance * movementSpeed;
            position.Y += yDistance * movementSpeed;
        }
    }
}
