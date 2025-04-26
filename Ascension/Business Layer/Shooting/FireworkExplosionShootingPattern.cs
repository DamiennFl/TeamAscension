// <copyright file="FireworkExplosionShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that shoots bullets in a firework explosion pattern.
    /// </summary>
    public class FireworkExplosionShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a firework explosion pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int bursts = 5;
            int bulletsPerBurst = 15;
            float baseSpeed = 2f;

            for (int burst = 0; burst < bursts; burst++)
            {
                float angleOffset = Random.Shared.NextSingle() * MathF.PI * 2;

                for (int i = 0; i < bulletsPerBurst; i++)
                {
                    float angle = (i * (MathF.PI * 2 / bulletsPerBurst)) + angleOffset;
                    float speed = baseSpeed + Random.Shared.NextSingle();
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
                    shooter.FireBullet(bulletVelocity);
                }
            }
        }
    }
}
