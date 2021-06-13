using System;
using System.Collections.Generic;
using DataContaner.RuntimeSets;
using Entities.Enemy;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.PlayerLoop;


namespace LevelManager
{
    public class Room : MonoBehaviour
    {
        public LevelManager lvManager => LevelManager.Instance;
        public List<SpawnPoints> spawns;
        public List<SpawnWaves> waves;
        public GameObject door;
        public int ID;


        private void OnEnable()
        {
            foreach (var points in GetComponentsInChildren<SpawnPoints>())
            {
                spawns.Add(points);
            }
        }

        private void OnDisable()
        {
            spawns.Clear();
        }

        public void OnTriggerEnter(Collider other)
        { 
            if(other.CompareTag("Player"))
            {
                door.GetComponent<Renderer>().enabled = true;
                door.GetComponent<Collider>().enabled = true;
                lvManager.currentRoom = this;
                lvManager.SpawnEnemies();
                lvManager.doorOpen = false;
            }
           
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}