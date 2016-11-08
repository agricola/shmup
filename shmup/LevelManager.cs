using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shmup.Enemies;
using shmup.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    class LevelManager
    {
        private Player player;
        private Texture2D enemyTexture;
        private List<Enemy> enemies = new List<Enemy>();
        private BulletManager bulletManager;
        private Vector2 mapDimensions;

        public void Initialize(Player player, BulletManager bulletManager, Texture2D enemyTexture, Vector2 mapDimensions)
        {
            this.player = player;
            this.bulletManager = bulletManager;
            this.enemyTexture = enemyTexture;
            this.mapDimensions = mapDimensions;
            CreateEnemies();
        }

        private void CreateEnemies()
        {
            // creating a movement list, is there a more elegant way?

            for (int i = 0; i < 3; i++)
            {
                List<EnemyAction> actionQueue = new List<EnemyAction>();
                float s = (float)Math.Sqrt(2);
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(2, 0), false); }));
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(s, -s), true); }));
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(0, -2), false); }));
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(-s, -s), true); }));
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(-2, 0), false); }));
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(-s, s), true); }));
                actionQueue.Add(new EnemyAction(500, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(0, 2), false); }));

                Vector2 enemyStartPosition = new Vector2(100, 200 + i * 100);
                Enemy enemy = new Enemy();
                enemy.Initialize(enemyTexture, enemyStartPosition, bulletManager, mapDimensions, actionQueue);
                enemies.Add(enemy);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.Exists)
                {
                    enemy.Update(gameTime);   // move to EnemyManager
                    bulletManager.CheckCollisionWithBullets(enemy);
                }
            }
            bulletManager.CheckCollisionWithBullets(player);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.Exists) enemy.Draw(spriteBatch);    // move to EnemyManager
            }
        }
    }
}
