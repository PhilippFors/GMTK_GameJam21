using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    public abstract void TakeDamage(float dmg);
    public abstract void Heal(float value);
}
