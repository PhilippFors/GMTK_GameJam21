using UnityEngine;
using Entities.Enemy;

namespace Attachments
{
    
    public class AttachmentBase : ScriptableObject
    {   
        public DamageType DamageType => damageType;
        [SerializeField] private DamageType damageType;
    }
}