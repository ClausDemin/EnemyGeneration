using System;

namespace Assets.Codebase.ObjectPool.Interfaces
{
    public interface IPooledInstance
    {
        public event Action<IPooledInstance> Released;
        public event Action<IPooledInstance> Disposed;

        public bool IsFree { get; }
    }

}
