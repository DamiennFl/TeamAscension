using Ascension.Enemies.EnemyMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Movement
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
                    return new CircularMovementPattern();
                case "ZigZag":
                    return new ZigZagMovementPattern();
                default:
                    throw new NotImplementedException($"No Movement Pattern by name: {type}");
            }
        }
    }
}