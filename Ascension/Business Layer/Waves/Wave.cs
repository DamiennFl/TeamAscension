using Ascension.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Waves
{
    internal abstract class Wave
    {
        // Time for the Wave
        public int Time { get; set; }

        private List<EnemyFormation> waveFormations = new List<EnemyFormation>();
    }
}