using Assets.Codebase.ObjectPool;
using Assets.Codebase.ObjectPool.Infrastructure;
using Assets.Codebase.ObjectPool.Interfaces;
using UnityEngine;

public abstract class MonoBehaviourSpawner<T>: MonoBehaviour
    where T: MonoBehaviour, IPooledInstance
{
    private Pool<T> _pool;

    [SerializeField] protected T _prefab;
    [SerializeField, Min(0)] protected int _prewarmedEntitiesCount; 

    protected virtual void Awake()
    {
        _pool = new Pool<T>(new MonoBehaviourFactory<T>(_prefab), _prewarmedEntitiesCount);
    }

    public virtual T SpawnInstance(Vector3 position) 
    {
        T instance = _pool.Get();
        
        instance.transform.position = position;

        return instance;
    }
}
