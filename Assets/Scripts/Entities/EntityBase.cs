using UnityEngine;

namespace Entities
{
    public abstract class EntityBase : MonoBehaviour
    {
        public abstract void TakeDamage(float dmg);
        public abstract void Heal(float value);
    }
}
