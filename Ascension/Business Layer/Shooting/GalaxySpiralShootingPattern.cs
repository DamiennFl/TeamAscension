// <copyright file="GalaxySpiralShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that shoots bullets in a spiral pattern.
    /// </summary>
    internal class GalaxySpiralShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a spiral pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int totalBullets = 100;
            float angleIncrement = MathF.PI * 10 / totalBullets; // Multiple rotations
            float radiusIncrement = 0.5f;
            float currentRadius = 0f;

            for (int i = 0; i < totalBullets; i++)
            {
                float angle = i * angleIncrement;
                currentRadius += radiusIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * currentRadius * 0.05f;
                shooter.FireBullet(bulletVelocity);
            }
        }
    }
}
