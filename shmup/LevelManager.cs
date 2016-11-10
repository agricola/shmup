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
        public List<Vector2> MoveQueue { get; private set; }
        public List<int> ShootQueue { get; private set; }
        public int SpawnDelay { get; private set; }

        public EnemySpawn(Texture2D enemyTexture, Vector2 position, List<Vector2> moveQueue, List<int> shootQueue, int spawnTime)
        {
            EnemyTexture = enemyTexture;
            Position = position;
            MoveQueue = moveQueue;
            ShootQueue = shootQueue;
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
                float s = (float)Math.Sqrt(2);
                int lat = 0;
                Vector2 enemyStartPosition = new Vector2(-50, lat);
                List<Vector2> moveQueue = new List<Vector2>() { new Vector2(mapDimensions.X / 2, 200), new Vector2(mapDimensions.X + 100, 0) };
                List<int> shootQueue = new List<int>() { 1000, 300, 300, 300, 1000, 300, 300, 300 };
                //Enemy enemy = new Enemy();
                EnemySpawn enemySpawn = new EnemySpawn(enemyTexture, enemyStartPosition, moveQueue, shootQueue, 1000);
                enemySpawnQueue.Add(enemySpawn);
            }
        }

        private void CreateEnemy(EnemySpawn enemySpawn)
        {
            Enemy enemy = new Enemy();
            enemy.Initialize(enemySpawn.EnemyTexture, enemySpawn.Position, bulletManager, mapDimensions, enemySpawn.MoveQueue, enemySpawn.ShootQueue);
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
