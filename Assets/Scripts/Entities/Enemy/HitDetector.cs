using System;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemy
{
    public class HitDetector : MonoBehaviour
    {
        [SerializeField] private float damage = 5f;
        public GameObject Player => player;
        private GameObject player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerBase>().TakeDamage(damage);
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
