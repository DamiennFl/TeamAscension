using Ascension.Business_Layer.Movement;
using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    public class WaitPattern : IMovementPattern
    {
        private bool isComplete;

        public void Move(GameTime gameTime, IMovable movable, float duration)
        {
            int timeWaited = 0;
            Vector2 stayPosition = movable.Position;
            while (timeWaited < duration)
            {
                movable.Position = stayPosition;
            }

            this.isComplete = true;
        }

        public bool IsComplete()
        {
            return this.isComplete;
        }
    }
}