using DataContainer.TypeReference.SOVariables;
using NaughtyAttributes;
using UnityEngine;

namespace DataContainer.TypeReference
{
    /// <summary>
    /// Can be used to store either a type variable or a custom local value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public abstract class TypeReference<T>
    {
        [SerializeField] private bool useLocalValue = false;

        [SerializeField,
         EnableIf("useLocalValue"), AllowNesting]
        private T localValue;

        [SerializeField,
         DisableIf("useLocalValue"), AllowNesting]
        private ScriptableObjectVariable<T> variable;

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