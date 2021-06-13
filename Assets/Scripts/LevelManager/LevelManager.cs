using System;
using System.Collections.Generic;
using DataContaner.RuntimeSets;
using General.Utilities;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace LevelManager
{
    public class LevelManager: SingletonBehaviour<LevelManager>
    {
        public Room currentRoom;
        public SpawnWaves currentWave;
        public int waveCount = 0;
        public List<GameObject> currentEnemies;
        public List<GameObject> instantiatedEnemies;
        public List<GameObject> doors;
        public bool doorOpen = true;
        public int roomCount;
        
        
        
        [Button]
        public void SpawnEnemies()
        {
            currentWave = currentRoom.waves[waveCount];
            waveCount++;
            for (int i = 0; i < currentWave.enemyAmount; i++)
            {
                if (Random.value < currentWave.heavySpawnRate)
                {
                    currentEnemies.Add(currentWave.fatEnemy);
                    continue;
                }

                if (Random.value < currentWave.fastSpawnRate)
                {
                   
                    currentEnemies.Add(currentWave.FastEnemy);
                    continue;
                }
                currentEnemies.Add(currentWave.baseEnemy);
            }

            foreach (var enemy in currentEnemies)
            {
               
                instantiatedEnemies.Add(Instantiate(enemy, currentRoom.spawns[Random.Range(0, currentRoom.spawns.Count - 1)].transform.position, Quaternion.identity));
            }
        }
        
        public bool allDead()
        {
            return instantiatedEnemies.Count == 0;
        }

        private void Update()
        {
            if (allDead() && !doorOpen)
            {
                if(currentRoom.waves.Count - 1 < waveCount)
                {
                    waveCount = 0;
                    doors[currentRoom.ID].gameObject.GetComponent<Renderer>().enabled = false;
                    doors[currentRoom.ID].gameObject.GetComponent<Collider>().enabled = false;
                    doorOpen = true;

                }
                else
                {
                    SpawnEnemies();
                }
            }
        }

        [Button()]
        void ClearList()
        {
            foreach (var enemy in instantiatedEnemies)
            {
                Destroy(enemy);
            }
            instantiatedEnemies.Clear();
            currentEnemies.Clear();
        }
    }
}