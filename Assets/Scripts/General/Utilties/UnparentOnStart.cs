using UnityEngine;

namespace Utilities
{
    public class UnparentOnStart : MonoBehaviour
    {
        private void Awake()
        {
            transform.DetachChildren();
        }
    }
}
