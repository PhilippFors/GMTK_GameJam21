using UnityEngine;

namespace Attachments
{
    public class StatusEffectAttachment : ScriptableObject, IAttachment
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