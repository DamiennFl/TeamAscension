using Ascension.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Waves;
using Microsoft.Xna.Framework;
using Ascension.Business_Layer.Movement;

namespace Ascension.Business_Layer.Enemies
{
    internal class EnemyManager
    {
        private EnemyFactory factory;
        private MovementFactory MovementFactory;

        public List<Enemy> Enemies { get; }

        private List<Wave> waves;

        // Stuff passed in from Wave
        public EnemyManager(EnemyFactory factory)
        {
            this.factory = factory;
            this.Enemies = new List<Enemy>();
        }

        // Spawn enemies for a duration, at an interval.
        private void SpawnEnemy(float duration, float spawnInterval, Enemy enemyToSpawn)
        {

        }

        // Remove enemies from this list
        private void KillEnemies()
        {

        }


        public void ProcessWaves(GameTime gameTime)
        {
            // Main Wave Manager
            foreach (Wave wave in waves)
            {
                int timeElapsed = 0;
                while (timeElapsed < wave.Duration)
                {
                    // All logic
                }
            }
        }

        private void Update(GameTime gameTime)
        {

        }

    }
}
