using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using shmup.Players;
using shmup.Enemies;
using System.Collections.Generic;
using System.Diagnostics;

namespace shmup
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private PlayerController playerController;
        private BulletManager bulletManager;
        private Enemy enemy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // player stuff
            player = new Player();
            playerController = new PlayerController();
            bulletManager = new BulletManager();

            //enemy stuff
            enemy = new Enemy();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Vector2 mapDimensions = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // bullet related content
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            bulletManager.Initialize(bulletTexture, 5, mapDimensions, true, player);

            // player related content
            Vector2 startPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 16, GraphicsDevice.Viewport.Height - 50);
            Texture2D texture = Content.Load<Texture2D>("player");
            player.Initialize(texture, startPosition, bulletManager);
            playerController.Initialize(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Z, mapDimensions, player);

            // enemy related content
            Texture2D enemyTexture = Content.Load<Texture2D>("enemy");
            Vector2 enemyStartPosition = startPosition - new Vector2(0, 100);

            // creating a movement list, is there a more elegant way?
            List<EnemyAction> actionQueue = new List<EnemyAction>();
            float s = (float)Math.Sqrt(2);
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(2, 0), false); }));
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(s, -s), true); }));
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(0, -2), false); }));
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(-s, -s), true); }));
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(-2, 0), false); }));
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(-s, s), true); }));
            actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(0, 2), false); }));
            enemy.Initialize(enemyTexture, enemyStartPosition, bulletManager, mapDimensions, actionQueue);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            playerController.Update(gameTime);
            bulletManager.Update(gameTime);
            if (enemy.Exists) enemy.Update(gameTime);   // move to EnemyManager
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            bulletManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            if (enemy.Exists) enemy.Draw(spriteBatch);    // move to EnemyManager
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
