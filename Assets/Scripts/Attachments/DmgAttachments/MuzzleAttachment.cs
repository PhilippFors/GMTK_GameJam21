using UnityEngine;

namespace Attachments.DamageAttachments
{
    [CreateAssetMenu]
    public class MuzzleAttachment : DamageAttachment
    {
        public float BulletSpeed => bulletSpeed;
        [SerializeField] private float bulletSpeed;
    }
}
