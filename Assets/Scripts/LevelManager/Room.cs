using System;
using System.Collections.Generic;
using DataContaner.RuntimeSets;
using Entities.Enemy;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace LevelManager
{
    public class Room : MonoBehaviour
    {
        
        public List<SpawnWaves> wavesList;

        public List<SpawnPoints> spawns;
        
        private bool roomStarted;
        public bool roomFinished;

        public int waveCount = 0;
        SpawnWaves currentWave;

        [Button]
        void SpawnEnemies()
        {
            currentWave = wavesList[waveCount];
            for (int i = 0; i < currentWave.enemyAmount; i++)
            {
                if (Random.value < currentWave.heavySpawnRate)
                {
                    //wave.currentEnemies.Add();
                    return;
                }

                if (Random.value < currentWave.fastSpawnRate)
                {
                    //wave.currentEnemies.Add();
                    return;
                }
                currentWave.currentEnemies.Add(currentWave.baseEnemy);
                
            }

            foreach (var enemy in currentWave.currentEnemies)
            {
               
                Instantiate(enemy, spawns[Random.Range(0, spawns.Count - 1)].transform);
            }
        }

        private void OnDisable()
        {
            currentWave.currentEnemies.Clear();
        }

        private void Update()
        {
            if (roomStarted)
            {
                if (currentWave.currentEnemies.Count == 0 )
                {
                    if(waveCount != wavesList.Count)
                    {
                        //SpawnEnemies();
                        waveCount++;
                    }
                    else
                    {
                        roomFinished = true;
                    }
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                SpawnEnemies();
                waveCount++;
                roomStarted = true;
                LevelManager.Instance.currentRoom = this;
            }
        }
    }
}