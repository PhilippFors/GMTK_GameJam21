using UnityEngine;

namespace Attachments
{
    public class StatusEffectAttachment : AttachmentBase
    {
        [SerializeField] private StatusEffectType type;
    }

    public enum StatusEffectType
    {
        Slow,
        DOT,
        EnemyExplode
    }
}