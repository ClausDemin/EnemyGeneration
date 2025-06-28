using Assets.Codebase.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Codebase.Spawner
{
    public class SkeletonSpawner: MonoBehaviourSpawner<Skeleton>
    {
        [SerializeField] private float _spawnInterval;

        [field: SerializeField] public Target Target { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(Spawn());
        }

        public override Skeleton SpawnInstance(Vector3 position)
        {
            Skeleton instance = base.SpawnInstance(position);

            instance.SetTarget(Target);

            instance.gameObject.SetActive(true);

            return instance;
        }

        public void SetTarget(Target target)
        {
            Target = target;
        }

        private IEnumerator Spawn() 
        {
            while (isActiveAndEnabled) 
            { 
                SpawnInstance(transform.position);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}
