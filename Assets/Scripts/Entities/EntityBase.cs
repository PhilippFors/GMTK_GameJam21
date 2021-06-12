using System;
using UnityEngine;

namespace Entities
{
    public abstract class EntityBase : MonoBehaviour
    {
        [SerializeField] protected float maxHealth;
        protected float currentHealth;
        public abstract void TakeDamage(float dmg);
        public abstract void Heal(float value);
        public abstract void OnDeath();
        private void Awake()
        {
            currentHealth = maxHealth;
        }
    }
}
