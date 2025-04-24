// <copyright file="XCrossShootingPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Shooting pattern that shoots bullets in an X-cross pattern.
    /// </summary>
    public class XCrossShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in an X-cross pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int arms = 4; // Four diagonal directions
            int bulletsPerArm = 6; // Each arm has three bullets
            float baseSpeed = 3f;
            float angleOffset = MathF.PI / 4; // 45-degree diagonal arms

            for (int arm = 0; arm < arms; arm++)
            {
                float angle = (arm * MathF.PI / 2) + angleOffset; // 90-degree separations

                for (int j = 0; j < bulletsPerArm; j++)
                {
                    float speedMultiplier = 1f + (j * 0.3f); // Vary speed for staggered effect
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (baseSpeed * speedMultiplier);
                    shooter.FireBullet(bulletVelocity);
                }
            }
        }
    }
}
