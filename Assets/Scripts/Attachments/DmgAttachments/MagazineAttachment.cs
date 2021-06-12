using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attachments.DamageAttachments
{
    [CreateAssetMenu]
    public class MagazineAttachment : DamageAttachment
    {
        public float FireRate => fireRate;
        [SerializeField] private float fireRate;
    }
}
