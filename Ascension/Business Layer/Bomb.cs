using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Business_Layer;

namespace Ascension
{
    public class Bomb
    {
        // Our enemy manager
        private EnemyManager enemyManager;

        // Our bullet manager
        private BulletManager bulletManager;

        /// <summary>
        /// Gets or sets the amount of damage a bomb can do.
        /// </summary>
        private int DamageCount { get; set; } = 3;

        public Bomb(EnemyManager enemyManager, BulletManager bulletManager)
        {
            this.enemyManager = enemyManager;
            this.bulletManager = bulletManager;
        }


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
