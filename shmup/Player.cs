using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Vector2 position;
        public int Height
        {
            get { return texture.Height; }
        }
        public int Width
        {
            get { return texture.Width; }
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

        private int FindCenter(int x, int y)
        {
            return 1 + ((x - 1) / y);
        }
    }
}
