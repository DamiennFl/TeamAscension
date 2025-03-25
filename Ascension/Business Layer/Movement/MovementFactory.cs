// <copyright file="MovementFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;

namespace Ascension
{
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
                default:
                    throw new NotImplementedException($"No Movement Pattern by name: {type}");
            }
        }
    }
}