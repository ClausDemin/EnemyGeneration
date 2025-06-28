using Assets.Codebase.Enemy.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase
{
    public class Target : MonoBehaviour, IMovable
    {
        private WaypointMovement<Target> _waypointMovement;
        [SerializeField] private List<Vector3> _path;

        private void Awake()
        {
            _waypointMovement = new WaypointMovement<Target>(this, _path);    
        }

        private void Start()
        {
            StartCoroutine(Move());
        }

        public bool CanMove => true;

        [field: SerializeField, Min(0)] public float MovementSpeed { get; private set; }

        public IEnumerator Move()
        {
            while (isActiveAndEnabled) 
            {
                if (CanMove) 
                {
                    foreach (Vector3 movement in _waypointMovement.GetMovement()) 
                    { 
                        transform.forward = movement;
                        transform.position += transform.forward * MovementSpeed * Time.deltaTime;

                        yield return null;
                    }
                }

                yield return null;
            }
        }
    }
}
