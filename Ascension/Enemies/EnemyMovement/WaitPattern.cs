using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    internal class WaitPattern : IMovementPattern
    {
        private float waitTime;
        private float elapsedTime

        // is complete is set to false by default
        private bool isComplete;

        public WaitPattern(float waitTime)
        {
            this.waitTime = waitTime;
            this.elapsedTime = 0f;
            this.isComplete = false;
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.elapsedTime >= this.waitTime)
            {
                this.isComplete = true;
            }
        }

        public bool IsComplete()
        {
            return this.isComplete;
        }
    }
}