using System;

namespace Ascension
{
    internal class MovementFactory
    {
        public MovementFactory() { }

        public IMovementPattern CreateMovementPattern(string type, float duration)
        {
            switch (type)
            {
                case "Linear":
                    return new LinearMovementPattern();
                case "Circular":
                    return new WaveMovementPattern();
                case "ZigZag":
                    return new ZigZagMovementPattern();
                default:
                    throw new NotImplementedException($"No Movement Pattern by name: {type}");
            }
        }
    }
}