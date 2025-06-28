using System.Collections;
using UnityEngine;

namespace Assets.Codebase.Enemy.Interfaces
{
    public interface IMovable
    {
        public bool CanMove { get; }
        public float MovementSpeed { get; }

        public IEnumerator Move();
    }
}
