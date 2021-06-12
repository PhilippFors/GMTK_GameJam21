using UnityEngine;

namespace Attachments.DamageAttachments
{
    public class MuzzleAttachment : DamageAttachment
    {
        public float BulletSpeed => bulletSpeed;
        [SerializeField] private float bulletSpeed;
    }
}
