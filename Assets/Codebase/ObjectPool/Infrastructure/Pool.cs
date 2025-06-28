using Assets.Codebase.ObjectPool.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.ObjectPool
{
    public class Pool<T>
        where T : MonoBehaviour, IPooledInstance
    {
        private IFactory<T> _factory;

        private Queue<T> _items;

        public Pool(IFactory<T> factory, int prewarmedCount = 0, bool isActive = false)
        {
            _items = new Queue<T>();
            _factory = factory;

            if (prewarmedCount > 0)
            {
                CreatePrewarmedInstances(prewarmedCount, isActive);
            }
        }

        public T Get()
        {
            _items.TryDequeue(out T freeObject);

            if (freeObject == null)
            {
                freeObject = Create();
            }

            return freeObject;
        }

        private T Create()
        {
            T instance = _factory.Create();

            instance.Released += OnRelease;
            instance.Disposed += OnDispose;

            return instance;
        }

        private void OnRelease(IPooledInstance instance)
        {
            _items.Enqueue(instance as T);
        }

        private void CreatePrewarmedInstances(int count, bool isActive)
        {
            for (int i = 0; i < count; i++)
            {
                T item = Create();

                item.gameObject.SetActive(isActive);
            }
        }

        private void OnDispose(IPooledInstance instance)
        {
            instance.Released -= OnRelease;
            instance.Disposed -= OnDispose;

            Remove(instance);
        }

        private void Remove(IPooledInstance instance)
        {
            Queue<T> items = new Queue<T>();

            while (_items.Count > 0)
            {
                T item = _items.Dequeue();

                if (item != instance)
                {
                    items.Enqueue(item);
                }
            }

            _items = items;
        }
    }
}

