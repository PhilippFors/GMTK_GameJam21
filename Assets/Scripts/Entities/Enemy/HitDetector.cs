using System;
using Effects;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemy
{
    public class HitDetector : MonoBehaviour
    {
        [SerializeField] private float damage = 5f;
        [SerializeField] private SoundEffectController sfx;
        public GameObject Player => player;
        private GameObject player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerBase>().TakeDamage(damage);
            }

            if (other.tag != "Enemy")
            {
                sfx.PlayEffect();
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
