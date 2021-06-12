using System.Collections.Generic;
using Entities.Enemy;
using UnityEngine;

namespace LevelManager
{
    [CreateAssetMenu]
    public class SpawnWaves: ScriptableObject
    {
        public float heavySpawnRate;
        public float fastSpawnRate;
        public float enemyAmount;
        public List<EnemyBase> currentEnemies;
    }
}