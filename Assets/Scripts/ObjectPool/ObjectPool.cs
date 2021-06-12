using General.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ObjectPool
{
    public class ObjectPool<T> : SingletonBehaviour<ObjectPool<T>> where T : Component
    {
        [SerializeField, Range(1, 500)] private int initialSize;
        [SerializeField] private T prefab;
        private GameObject parent;
        private Queue<T> pool = new Queue<T>();

        private void Awake()
        {
            parent = new GameObject($"{typeof(T)}-ObjectPool");

            for (int i = 0; i < initialSize; i++)
            {
                var obj = InstantiateObject(false);
                pool.Enqueue(obj);
            }
        }

        public T GetObject()
        {
            if (pool.Count == 0)
            {
                return InstantiateObject(false);
            }

            var obj = pool.Dequeue();
            obj.transform.parent = null;
            return obj;
        }

        public void ReleaseObject(T obj)
        {
            obj.transform.parent = parent.transform;
            obj.gameObject.SetActive(false);
            
            Assert.IsFalse(pool.Contains(obj), "Trying to release object multiple times.");

            pool.Enqueue(obj);
        }

        private T InstantiateObject(bool setActive)
        {
            var obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(setActive);
            obj.transform.parent = parent.transform;
            return obj;
        }
    }
}
