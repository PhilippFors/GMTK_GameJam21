using System.Collections.Generic;
using Attachments;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyBase : EntityBase
    {
        public float MovementSpeed => movementSpeed;
        public List<DamageResistance> DamageResistances => damageResistances;
        public DamageType DamageType => damageType;
        [SerializeField] private DamageType damageType;
        [SerializeField] private List<DamageResistance> damageResistances = new List<DamageResistance>();
        [SerializeField] private float movementSpeed;
        
        public override void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
            if (currentHealth <= 0)
            {
                OnDeath();
            }
        }

        public override void Heal(float value)
        {
        }

        public override void OnDeath()
        {
            Destroy(gameObject);
        }
    }

    public enum DamageType
    {
        red,
        blue,
        green,
        player
    }
}