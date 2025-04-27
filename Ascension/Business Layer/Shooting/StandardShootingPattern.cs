using Ascension.Business_Layer.Bullets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Standard shooting pattern that shoots bullets in a straight line.
    /// </summary>
    public class StandardShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a straight line.
        /// </summary>
        /// <param name="shooter">Shooter.</param>
        public void Shoot(IEntity shooter)
        {
            Vector2 bulletVelocity = new Vector2(0, 2); // downward shooting for enemy
            if (shooter.IsPlayer)
            {
                bulletVelocity = new Vector2(0, -7f); // upward shooting for player
            }

            shooter.FireBullet(bulletVelocity);
        }
    }
}
