// <copyright file="WavePatternShoot.cs" company="Team Ascension">
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
    /// Shooting pattern that shoots bullets in a wave pattern.
    /// </summary>
    public class WaveShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a wave pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int bulletsPerWave = 15;
            float waveCount = 3;
            float amplitude = 1.5f;
            float frequency = 0.3f;

            for (int wave = 0; wave < waveCount; wave++)
            {
                for (int i = -bulletsPerWave / 2; i < bulletsPerWave / 2; i++)
                {
                    float x = i * 0.2f;
                    float y = amplitude * MathF.Sin(frequency * x + wave);
                    Vector2 bulletVelocity = new Vector2(x, y + 2f);

                    // Reverse direction if the shooter is a player
                    if (shooter.IsPlayer)
                    {
                        bulletVelocity = -bulletVelocity;
                    }

                    shooter.FireBullet(bulletVelocity);
                }
            }
        }
    }
}
