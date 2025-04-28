// <copyright file="IShootingPattern.cs" company="Team Ascension">
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
    /// Interface for shooting patterns.
    /// </summary>
    public interface IShootingPattern
    {
        /// <summary>
        /// Shoots a bullet.
        /// </summary>
        /// <param name="shooter"> The entity that is shooting.</param>
        void Shoot(IEntity shooter);
    }
}
