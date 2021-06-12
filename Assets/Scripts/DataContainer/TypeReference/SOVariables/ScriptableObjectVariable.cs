using UnityEngine;

namespace DataContainer.TypeReference.SOVariables
{
    /// <summary>
    /// Simple ScriptableObject based variable which can be shared across any number of classes and scenes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ScriptableObjectVariable<T> : ScriptableObject
    {
        public T value;
    }
}
