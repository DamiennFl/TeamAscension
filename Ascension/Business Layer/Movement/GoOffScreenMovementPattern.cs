using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Movement
{
    internal class GoOffScreenMovementPattern : IMovementPattern
    {
        public void Move(GameTime gameTime, IMovable movable)
        {
            movable.Velocity = new Vector2(-1, -1);
            movable.Position += movable.Velocity;
        }
    }
}
