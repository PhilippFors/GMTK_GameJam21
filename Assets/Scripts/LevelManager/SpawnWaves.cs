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
        public List<GameObject> currentEnemies;
        public GameObject baseEnemy;
    }
}