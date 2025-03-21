﻿using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Movement
{
    internal interface IMovable
    {
        Vector2 Position { get; set; }

        Vector2 Velocity { get; set; }
    }
}