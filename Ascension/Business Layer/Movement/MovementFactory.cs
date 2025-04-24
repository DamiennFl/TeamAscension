// <copyright file="MovementFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Business_Layer.Movement;
using System;

namespace Ascension
{
    /// <summary>
    /// Factory class for creating movement patterns.
    /// </summary>
    internal class MovementFactory
    {
        public MovementFactory() { }

        public IMovementPattern CreateMovementPattern(string type)
        {
            switch (type)
            {
                case "Linear":
                    return new LinearMovementPattern();
                case "Wave":
                    return new WaveMovementPattern();
                case "ZigZag":
                    return new ZigZagMovementPattern();
                case "GoMiddle":
                    return new GoMiddleMovementPattern();
                case "GoOffScreen":
                    return new GoOffScreenMovementPattern();
                default:
                    throw new NotImplementedException($"No Movement Pattern by name: {type}");
            }
        }
    }
}