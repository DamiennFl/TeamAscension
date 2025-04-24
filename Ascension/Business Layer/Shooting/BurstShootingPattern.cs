using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Shooting pattern that shoots bullets in a burst pattern.
    /// </summary>
    public class BurstShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a burst pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            if (shooter.IsPlayer)
            {
                float angle = 0.9f;
                for (int i = 0; i < 3; i++)
                {
                    var dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    shooter.FireBullet(dir);
                    angle -= 0.9f;
                }
            }
            else
            {
                float angle = -0.9f;
                for (int i = 0; i < 3; i++)
                {
                    var dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    shooter.FireBullet(dir);
                    angle += 0.9f;
                }
            }
        }
    }
}
