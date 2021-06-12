using System;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemy
{
    public class HitDetector : MonoBehaviour
    {
        public GameObject Player => player;
        private GameObject player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                player = null;
            }
        }
    }
}
