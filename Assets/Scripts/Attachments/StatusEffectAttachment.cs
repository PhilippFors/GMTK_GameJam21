using Entities.Enemy;
using UnityEngine;

namespace Attachments
{
    public abstract class StatusEffectAttachment : AttachmentBase
    {
        public StatusEffectType StatusType => statusType;
        [SerializeField] private StatusEffectType statusType;
        [SerializeField] protected float effectCooldown;
        [SerializeField] protected float strength;
        public abstract void ApplyEffect(EnemyBase enemy);

        protected float CalcStrength(EnemyBase enemy, float strength)
        {
            if (enemy.DamageType != DamageType)
            {
                var resistance = enemy.DamageResistances.Find(x => x.DamageType == DamageType).Resistance;
                return strength * (1 - resistance);
            }

            return strength;
        }
    }

    public enum StatusEffectType
    {
        Slow,
        DOT,
        EnemyExplode
    }
}