using UnityEngine;
using Entities.Enemy;

namespace Attachments
{
    public class AttachmentBase : ScriptableObject
    {
        public Sprite UISprite => uiSprite;
        public int AttachmentID => attachmentID;
        public DamageType DamageType => damageType;
        [SerializeField] private DamageType damageType;
        [SerializeField] private int attachmentID;
        [SerializeField] private Sprite uiSprite;
    }
}