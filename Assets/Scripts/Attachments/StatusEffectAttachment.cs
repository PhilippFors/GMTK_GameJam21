using UnityEngine;

namespace Attachments
{
    [CreateAssetMenu]
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