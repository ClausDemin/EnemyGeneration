﻿using Assets.Codebase.ObjectPool.Interfaces;
using UnityEngine;

namespace Assets.Codebase.ObjectPool.Infrastructure
{
    public class MonoBehaviourFactory<T> : IFactory<T>
        where T : MonoBehaviour
    {
        private T _prefab;

        public MonoBehaviourFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create()
        {
            T instance = GameObject.Instantiate(_prefab);

            return instance;
        }
    }
}
