// <copyright file="Wave.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Ascension.Business_Layer.Shooting;
using Microsoft.Xna.Framework.Content;

namespace Ascension
{
    /// <summary>
    /// Wave Class that holds the info necessary for a Wave of Enemies.
    /// </summary>
    public class Wave
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wave"/> class.
        /// </summary>
        /// <param name="duration"> The duration of this Wave.</param>
        /// <param name="enemyType">The enemy type of this Wave.</param>
        /// <param name="enemyCount">The enemy count of this Wave.</param>
        /// <param name="spawnInterval">The spawn interval of Enemies of this Wave.</param>
        /// <param name="health">The health of the Enemies of this Wave.</param>
        /// <param name="movementPattern">The IMovementPattern of Enemies of this Wave.</param>
        /// <param name="bulletType">The bullet typed of Enemies of this Wave.</param>
        /// <param name="shootingPattern">The shooting pattern of Enemies of this Wave.</param>
        /// <param name="shotsPerSecond">The shots per second shot by Enemies of this Wave.</param>
        public Wave(float duration, string enemyType, int enemyCount, float spawnInterval, int health, string movementPattern, string bulletType, Dictionary<string, string> shootingPatterns)
        {
            this.Duration = duration;
            this.EnemyType = enemyType;
            this.EnemyCount = enemyCount;
            this.SpawnInterval = spawnInterval;
            this.Health = health;
            this.MovementPattern = movementPattern;
            this.BulletType = bulletType;
            this.ShootingPatterns = shootingPatterns;
        }

        /// <summary>
        /// Gets or sets the duration of this Wave.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Gets or sets the Enemy type of this Wave.
        /// </summary>
        public string EnemyType { get; set; }

        /// <summary>
        /// Gets or sets the Enemy count of this Wave.
        /// </summary>
        public int EnemyCount { get; set; }

        /// <summary>
        /// Gets or sets the spawn interval of this Wave.
        /// </summary>
        public float SpawnInterval { get; set; }

        /// <summary>
        /// Gets or sets the health of the Enemies in this Wave.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the IMovementPattern of Enemies of this Wave.
        /// </summary>
        public string MovementPattern { get; set; }

        /// <summary>
        /// Gets or sets the BulletType of Enemies of this Wave.
        /// </summary>
        public string BulletType { get; set; }

        /// <summary>
        /// Gets or sets dictionary of ShootingPatterns, with the key being the pattern and the value
        /// being the shots per second, either a float number or "Random".
        /// </summary>
        public Dictionary<string, string> ShootingPatterns { get; set; }
    }
}