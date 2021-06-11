using DataContainer.Variables;
using NaughtyAttributes;
using UnityEngine;

namespace DataContainer.TypeReference
{
    [System.Serializable]
    public abstract class TypeReference<T>
    {
        [SerializeField] private bool useLocalValue = false;

        [SerializeField,
         EnableIf("useLocalValue"), AllowNesting]
        private T localValue;

        [SerializeField,
         DisableIf("useLocalValue"), AllowNesting]
        private SOVariable<T> variable;

        public T Value
        {
            get => useLocalValue ? localValue : variable.value;
            set
            {
                if (useLocalValue)
                {
                    localValue = value;
                }
                else
                {
                    variable.value = value;
                }
            }
        }
    }
}