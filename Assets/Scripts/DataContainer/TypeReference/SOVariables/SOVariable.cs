using UnityEngine;

namespace DataContainer.Variables
{
    public abstract class SOVariable<T> : ScriptableObject
    {
        public T value;
    }
}
