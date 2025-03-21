using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Movement
{
    internal class ThereAndBackMovementPattern : IMovementPattern
    {
        private bool complete = false;

        public void Move(GameTime gameTime, IMovable movable, float duration)
        {
            int timeMoved = 0;
            Vector2 velocity = movable.Velocity;
            while (timeMoved < duration / 2)
            {
                movable.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeMoved += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            while (timeMoved < duration)
            {
                movable.Position -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeMoved += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            this.complete = true;
        }

        public bool IsComplete()
        {
            return this.complete;
        }
    }
}
