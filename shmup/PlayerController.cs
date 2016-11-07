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
    class PlayerController
    {
        private Player player;

        // Input keys
        private Keys moveLeftKey;
        private Keys moveRightKey;
        private Keys moveUpKey;
        private Keys moveDownKey;
        private Keys shootKey;

        // so controller knows when to disallow movement
        private Vector2 mapDimensions;

        // current state (keys pressed) of keyboard
        private KeyboardState keyboardState;

        public void Initialize(Keys moveLeftKey, Keys moveRightKey, Keys moveUpKey, Keys moveDownKey, Keys shootKey, Vector2 mapDimensions, Player player)
        {
            this.moveLeftKey = moveLeftKey;
            this.moveRightKey = moveRightKey;
            this.moveUpKey = moveUpKey;
            this.moveDownKey = moveDownKey;
            this.mapDimensions = mapDimensions;
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(moveLeftKey))
            {
                player.MovePlayer(-1, 0);
            }
            if (keyboardState.IsKeyDown(moveRightKey))
            {
                player.MovePlayer(1, 0);
            }
            if (keyboardState.IsKeyDown(moveUpKey))
            {
                player.MovePlayer(0, -1);
            }
            if (keyboardState.IsKeyDown(moveDownKey))
            {
                player.MovePlayer(0, 1);
            }

            // make sure the player is within the map
            Vector2 playerPosition = player.Position;
            float newX = MathHelper.Clamp(playerPosition.X, 0, mapDimensions.X - player.Width);
            float newY = MathHelper.Clamp(playerPosition.Y, 0, mapDimensions.Y - player.Height);
            player.Position = new Vector2(newX, newY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
