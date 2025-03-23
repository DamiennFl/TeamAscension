using Ascension.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Waves
{
    internal class Wave
    {
        public float Duration { get; set; }

        public string EnemyType { get; set; }

        public int EnemyCount { get; set; }

        public float SpawnInterval { get; set; }

        public int Health { get; set; }

        public string MovementPattern { get; set; }

        public Wave(float duration, string enemyType, int enemyCount, float spawnInterval, int health, string movementPattern)
        {
            this.Duration = duration;
            this.EnemyType = enemyType;
            this.EnemyCount = enemyCount;
            this.SpawnInterval = spawnInterval;
            this.Health = health;
            this.MovementPattern = movementPattern;
        }
    }
}