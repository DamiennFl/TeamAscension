using Microsoft.Xna.Framework;

namespace Ascension
{
    public interface IMovable
    {
        Vector2 Position { get; set; }

        Vector2 Velocity { get; set; }
    }
}