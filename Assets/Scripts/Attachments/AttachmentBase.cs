using UnityEngine;
using Entities.Enemy;

namespace Attachments
{
    
    public class AttachmentBase : ScriptableObject
    {
        public AttachmentID AttachmentID => attachmentID;
        public DamageType DamageType => damageType;
        [SerializeField] private DamageType damageType;
        [SerializeField] private AttachmentID attachmentID;
    }
}