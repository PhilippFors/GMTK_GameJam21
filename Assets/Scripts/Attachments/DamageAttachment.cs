using UnityEngine;

namespace Attachments
{
    public class DamageAttachment : AttachmentBase
    {
        [SerializeField] private float dmgMultiplier;
        [SerializeField] private DamageType damageType;
    }

    public enum DamageType
    {
        red,
        blue,
        green,
        player
    }
}
