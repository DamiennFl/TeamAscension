// <copyright file="Bomb.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Business_Layer;

namespace Ascension
{
    /// <summary>
    /// Bomb class for the player.
    /// </summary>
    public class Bomb
    {
        /// <summary>
        /// Gets or sets the enemyManager.
        /// </summary>
        private EnemyManager enemyManager;

        /// <summary>
        /// Gets or sets the enemyManager.
        /// </summary>
        private BulletManager bulletManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bomb"/> class.
        /// </summary>
        /// <param name="enemyManager"> our enemy manager.</param>
        /// <param name="bulletManager">our bullet manager.</param>
        public Bomb(EnemyManager enemyManager, BulletManager bulletManager)
        {
            this.enemyManager = enemyManager;
            this.bulletManager = bulletManager;
        }

        /// <summary>
        /// Gets or sets the amount of damage a bomb can do.
        /// </summary>
        private int DamageCount { get; set; } = 3;

        /// <summary>
        /// Our method for exploding the bomb,
        /// it will be based on user input in player.
        /// </summary>
        public void ExplodeBomb()
        {
            // Clears the screen of all bullets
            this.bulletManager.ClearScreen();

            foreach (var enemy in this.enemyManager.Enemies)
            {
                enemy.Health -= this.DamageCount;
            }
        }
    }
}
