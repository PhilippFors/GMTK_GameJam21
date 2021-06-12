using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attachments.DamageAttachments
{
    public class MagazineAttachment : DamageAttachment
    {
        public float FireRate => fireRate;
        [SerializeField] private float fireRate;
    }
}
