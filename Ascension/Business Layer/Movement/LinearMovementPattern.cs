using Microsoft.Xna.Framework;

namespace Ascension
{
    public class LinearMovementPattern : IMovementPattern
    {
        public void Move(GameTime gameTime, IMovable movable)
        {
            Vector2 velocity = movable.Velocity;
            movable.Position += velocity;
        }
    }
}