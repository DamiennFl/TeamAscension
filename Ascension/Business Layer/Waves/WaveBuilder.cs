// <copyright file="WaveBuilder.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ascension.Business_Layer.Shooting;

namespace Ascension.Business_Layer.Waves
{
    /// <summary>
    /// WaveBuilder creates the Waves based on the MainGame.json file.
    /// </summary>
    internal class WaveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaveBuilder"/> class.
        /// </summary>
        public WaveBuilder()
        {
            this.Waves = new List<Wave>();
        }

        /// <summary>
        /// Gets or sets a list of Waves.
        /// </summary>
        private List<Wave> Waves { get; set; }

        /// <summary>  
        /// Generates Waves based on the MainGame.json file.  
        /// </summary>  
        /// <returns>Returns a List of generated Waves.</returns>  
        public List<Wave> GenerateWaves()
        {
            string filePath = "C:\\Users\\Dillon\\Source\\Repos\\TeamAscension\\Ascension\\Business Layer\\Waves\\MainGame.json";
            string jsonContent = File.ReadAllText(filePath);

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

                    // Parse ShootingPatterns into a Dictionary<string, string>
                    JsonElement shootingPatternsElement = waveData.GetProperty("ShootingPatterns");
                    var shootingPatterns = new Dictionary<string, string>();

                    foreach (JsonProperty shootingPatternProperty in shootingPatternsElement.EnumerateObject())
                    {
                        string patternName = shootingPatternProperty.Name;
                        string interval = shootingPatternProperty.Value.GetString();

                        shootingPatterns.Add(patternName, interval);
                    }

                    // Create the Wave object
                    var wave = new Wave(
                        duration: duration,
                        enemyType: enemyType,
                        enemyCount: enemyCount,
                        spawnInterval: spawnInterval,
                        health: health,
                        movementPattern: movementPattern,
                        bulletType: bulletType,
                        shootingPatterns: shootingPatterns);

                    this.Waves.Add(wave);
                }

                return this.Waves;
            }
        }
    }
}
