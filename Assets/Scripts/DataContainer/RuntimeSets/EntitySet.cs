using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DataContaner.RuntimeSets
{
    /// <summary>
    /// Can contain anything at runtime and can be share across multiple classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntitySet<T> : ScriptableObject
    {
        private List<T> entityList = new List<T>();

        public void Add(T t)
        {
            Assert.IsFalse(entityList.Contains(t));

            entityList.Add(t);
        }

        public void Remove(T t)
        {
            entityList.Remove(t);
        }

        public void ClearList()
        {
            entityList.Clear();
        }

        private void OnDisable()
        {
            entityList.Clear();
        }
    }
}