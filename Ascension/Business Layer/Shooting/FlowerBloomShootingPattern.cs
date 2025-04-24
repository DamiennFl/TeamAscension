// <copyright file="FlowerBloomShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that shoots bullets in a flower bloom pattern.
    /// </summary>
    public class FlowerBloomShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a flower bloom pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int petals = 8;
            int bulletsPerPetal = 10;
            float growthSpeed = 1f;

            for (int petal = 0; petal < petals; petal++)
            {
                float angleOffset = petal * (MathF.PI * 2 / petals);

                for (int i = 0; i < bulletsPerPetal; i++)
                {
                    float progress = (float)(i + 1) / bulletsPerPetal; // Start from 1 to avoid bullet staying next to the enemy
                    float angle = angleOffset + (progress * MathF.PI / petals);
                    float radius = progress * 5f;
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius * growthSpeed;
                    shooter.FireBullet(bulletVelocity);
                }
            }
        }
    }
}
