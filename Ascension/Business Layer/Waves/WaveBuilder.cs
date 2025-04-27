using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Waves
{
    internal class WaveBuilder
    {

        private List<Wave> Waves { get; set; }

        public WaveBuilder()
        {
            this.Waves = new List<Wave>();
        }

        /// <summary>
        /// Reads the MainGame.json file and generates Wave objects.
        /// </summary>
        /// <summary>
        /// Reads the MainGame.json file and generates Wave objects.
        /// </summary>
        public void GenerateWaves()
        {
            string filePath = "Business Layer/Waves/MainGame.json";

            string jsonContent = File.ReadAllText(filePath);

            // Parse the JSON manually
            using (JsonDocument document = JsonDocument.Parse(jsonContent))
            {
                JsonElement root = document.RootElement;
                JsonElement waves = root.GetProperty("Game").GetProperty("Waves");

                foreach (JsonProperty waveProperty in waves.EnumerateObject())
                {
                    JsonElement waveData = waveProperty.Value;

                    // Extract wave properties
                    float duration = waveData.GetProperty("Duration").GetSingle();
                    string enemyType = waveData.GetProperty("EnemyType").GetString();
                    int enemyCount = waveData.GetProperty("EnemyCount").GetInt32();
                    float spawnInterval = waveData.GetProperty("SpawnInterval").GetSingle();
                    int health = waveData.GetProperty("Health").GetInt32();
                    string movementPattern = waveData.GetProperty("MovementPattern").GetString();
                    string bulletType = waveData.GetProperty("BulletType").GetString();

                    // Extract the first shooting pattern
                    JsonElement shootingPatterns = waveData.GetProperty("ShootingPatterns");
                    string shootingPattern = shootingPatterns.EnumerateObject().First().Name;

                    // Create the Wave object
                    var wave = new Wave(
                        duration: duration,
                        enemyType: enemyType,
                        enemyCount: enemyCount,
                        spawnInterval: spawnInterval,
                        health: health,
                        movementPattern: movementPattern,
                        bulletType: bulletType,
                        shootingPattern: shootingPattern
                    );

                    this.Waves.Add(wave);
                }
            }
        }
    }
}
}
