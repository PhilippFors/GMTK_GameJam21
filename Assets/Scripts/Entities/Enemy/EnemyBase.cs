using Attachments;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyBase : EntityBase
    {
        [SerializeField] private DamageType damageType;

        public override void TakeDamage(float dmg)
        {

        }

        public override void Heal(float value)
        {

        }
    }
}
