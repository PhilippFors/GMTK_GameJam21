using Entities.Enemy;
using UnityEngine;

namespace Attachments
{
    public abstract class StatusEffectAttachment : AttachmentBase
    {
        public StatusEffectType StatusType => statusType;
        [SerializeField] private StatusEffectType statusType;

        public abstract void ApplyEffect(EnemyBase enemy);
    }

    public enum StatusEffectType
    {
        Slow,
        DOT,
        EnemyExplode
    }
}