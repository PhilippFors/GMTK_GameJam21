using UnityEngine;
using Entities.Enemy;

namespace Attachments
{
    [CreateAssetMenu]
    public class DamageAttachment : AttachmentBase
    {
        public float DmgMultiplier => dmgMultiplier;
        [SerializeField] private float dmgMultiplier;
    }
}
