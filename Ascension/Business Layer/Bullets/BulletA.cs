// <copyright file="BulletA.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Business_Layer.Bullets
{
    /// <summary>
    /// BulletA class.
    /// </summary>
    internal class BulletA : Bullet
    {
        private static new LinearMovementPattern MovementPattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletA"/> class.
        /// </summary>
        /// <param name="damage">Damage of bullet.</param>
        /// <param name="velocity">Velocity of bullet.</param>
        /// <param name="bulletPosition">Position of bullet.</param>
        /// <param name="bulletTexture">Texture of bullet.</param>
        public BulletA(int damage, Vector2 velocity, Vector2 bulletPosition, Texture2D bulletTexture)
            : base(damage, velocity, bulletPosition)
        {
            this.Damage = damage;
            this.Velocity = velocity;
            this.BulletTexture = bulletTexture;
            this.IsActive = true;
            this.IsPlayerBullet = false;
        }
    }
}
