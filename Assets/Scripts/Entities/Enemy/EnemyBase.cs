using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyBase : EntityBase
    {
        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        public List<DamageResistance> DamageResistances => damageResistances;
        public DamageType DamageType => damageType;
        [SerializeField] private DamageType damageType;
        [SerializeField] private List<DamageResistance> damageResistances = new List<DamageResistance>();
        [SerializeField] private float movementSpeed;
        private bool alive = true;
        public override void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
            if (currentHealth <= 0)
            {
                if (alive)
                {
                    OnDeath();
                    alive = false;
                }
            }
        }

        public override void Heal(float value)
        {
        }

        public override void OnDeath()
        {
            var att = GetComponent<EnemyAttack>();
            if (att.CanExplodeOnDeath)
            {
                var surrounding = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Enemy", "Player"));
                foreach (var entity in surrounding)
                {
                    if (entity.GetComponent<EnemyBase>() != this)
                    {
                        entity.GetComponent<EntityBase>().TakeDamage(att.ExplodingStrength * att.Damage);
                        Debug.Log($"Damage to {entity.name}, {att.ExplodingStrength * att.Damage}");
                    }
                }

                // TODO: Play particle effect
            }

            // TODO: Play death animation
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