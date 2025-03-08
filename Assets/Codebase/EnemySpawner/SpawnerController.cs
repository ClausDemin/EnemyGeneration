using Assets.Codebase.Utils;
using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField, Min(1)] private float _spawnInterval;
    [SerializeField] private EnemySpawner[] _spawners;

    private void Start()
    {
        if (_spawners.Length > 0) 
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy() 
    {
        int spawnersCount = _spawners.Length;
        WaitForSeconds delay = new WaitForSeconds(_spawnInterval);

        while (isActiveAndEnabled) 
        {
            _spawners[Randomizer.GetRandomInt(0, spawnersCount)].SpawnInstance();

            yield return delay;
        }
    }

}
