using Ascension.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Waves
{
    internal abstract class Wave
    {
        // Time for the Wave
        public float Duration { get; set; }

        public string EnemyType { get; set; }

        public int EnemyCount { get; set; }

        public string EnemyMovementPattern { get; set; }

        public float SpawnInterval { get; set; }

        public Wave(float duration, , float? spawnInterval)
        {
            this.Duration = duration;
            this.EnemyType = EnemyType;
            if (spawnInterval != null)
            {
                this.SpawnInterval = (float)spawnInterval;
            }
            else
            {
                this.SpawnInterval = 0.2f;
            }
        }
    }
}