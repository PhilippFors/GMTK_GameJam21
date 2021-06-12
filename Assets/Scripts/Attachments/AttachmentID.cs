using UnityEngine;

namespace Attachments
{
    public class AttachmentID : MonoBehaviour
    {
        public int ID => id;
        [SerializeField] private int id;
    }
}
