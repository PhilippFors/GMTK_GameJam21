using UnityEngine;

namespace Attachments.DamageAttachments
{
    [CreateAssetMenu]
    public class MuzzleAttachment : DamageAttachment
    {
        public AudioClip SFX => sfx;
        public float BulletSpeed => bulletSpeed;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private AudioClip sfx;
    }
}
