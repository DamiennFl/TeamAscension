// <copyright file="ShootingPatternFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Factory class for creating shooting patterns.
    /// </summary>
    internal class ShootingPatternFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShootingPatternFactory"/> class.
        /// </summary>
        public ShootingPatternFactory() { }

        /// <summary>
        /// Creates a shooting pattern based on the type provided.
        /// </summary>
        /// <param name="type">Type provided.</param>
        /// <returns>Returns type of shooting pattern selected.</returns>
        public IShootingPattern CreateShootingPattern(string type)
        {
            switch (type)
            {
                case "Standard":
                    return new StandardShootingPattern();
                case "Burst":
                    return new BurstShootingPattern();
                case "Wall":
                    return new BulletWallShootingPattern();
                case "Circular":
                    return new CircularShootingPattern();
                case "Firework":
                    return new FireworkExplosionShootingPattern();
                case "Flower":
                    return new FlowerBloomShootingPattern();
                case "Galaxy":
                    return new GalaxySpiralShootingPattern();
                case "Wave":
                    return new WaveShootingPattern();
                case "XCross":
                    return new XCrossShootingPattern();
                case "TowardsPlayer":
                    return new TowardsPlayerShootingPattern();
                default:
                    return new StandardShootingPattern();
            }
        }
    }
}
