using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shmup.Enemies;
using shmup.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup
{
    struct EnemySpawn
    {
        public Texture2D EnemyTexture { get; private set; } //in future maybe have different enemy values instead of texture
        public Vector2 Position { get; private set; }
        public List<EnemyAction> ActionQueue { get; private set; }
        public int SpawnDelay { get; private set; }

        public EnemySpawn(Texture2D enemyTexture, Vector2 position, List<EnemyAction> actionQueue, int spawnTime)
        {
            EnemyTexture = enemyTexture;
            Position = position;
            ActionQueue = actionQueue;
            SpawnDelay = spawnTime;
        }
    }
    class LevelManager
    {
        private Player player;
        private Texture2D enemyTexture;
        private List<Enemy> enemies = new List<Enemy>();
        private List<EnemySpawn> enemySpawnQueue = new List<EnemySpawn>();
        private BulletManager bulletManager;
        private Vector2 mapDimensions;
        private double previousSpawnTime = 0;
        private Random random;

        public void Initialize(Player player, BulletManager bulletManager, Texture2D enemyTexture, Vector2 mapDimensions)
        {
            this.player = player;
            this.bulletManager = bulletManager;
            this.enemyTexture = enemyTexture;
            this.mapDimensions = mapDimensions;
            random = new Random();
            CreateEnemySpawns();
        }

        private void CreateEnemySpawns()
        {
            // creating a movement list, is there a more elegant way?

            for (int i = 0; i < 5; i++)
            {
                List<EnemyAction> actionQueue = new List<EnemyAction>();
                float s = (float)Math.Sqrt(2);
                // first delay doesnt matter, needs fixing
                actionQueue.Add(new EnemyAction(1000, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(2, 0), false); }));
                actionQueue.Add(new EnemyAction(1000, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(2, 0), true); }));
                actionQueue.Add(new EnemyAction(1000, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(2, 0), true); }));
                actionQueue.Add(new EnemyAction(1000, (Vector2 pos) => { return new Tuple<Vector2, bool>(pos + new Vector2(2, 0), false); }));
                Vector2 enemyStartPosition = new Vector2(-50, random.Next(50, 400));
                Enemy enemy = new Enemy();
                EnemySpawn enemySpawn = new EnemySpawn(enemyTexture, enemyStartPosition, actionQueue, 3000);
                enemySpawnQueue.Add(enemySpawn);
            }
        }

        private void CreateEnemy(EnemySpawn enemySpawn)
        {
            Enemy enemy = new Enemy();
            enemy.Initialize(enemySpawn.EnemyTexture, enemySpawn.Position, bulletManager, mapDimensions, enemySpawn.ActionQueue);
            enemies.Add(enemy);
        }

        public void Update(GameTime gameTime)
        {

            double totalMs = gameTime.TotalGameTime.TotalMilliseconds;
            if (totalMs - previousSpawnTime > enemySpawnQueue[0].SpawnDelay && enemySpawnQueue.Count > 1)
            {
                CreateEnemy(enemySpawnQueue[0]);
                enemySpawnQueue.RemoveAt(0);
                previousSpawnTime = totalMs;
            }

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
