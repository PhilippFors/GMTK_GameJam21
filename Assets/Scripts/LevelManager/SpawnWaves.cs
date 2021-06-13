using System.Collections.Generic;
using DataContaner.RuntimeSets;
using Entities.Enemy;
using UnityEngine;

namespace LevelManager
{
    [CreateAssetMenu]
    public class SpawnWaves: EntitySet<SpawnWaves>
    {
        public float heavySpawnRate;
        public float fastSpawnRate;
        public float enemyAmount;
        
        public GameObject baseEnemy;
        public GameObject fatEnemy;
        public GameObject FastEnemy;
        
    }
}