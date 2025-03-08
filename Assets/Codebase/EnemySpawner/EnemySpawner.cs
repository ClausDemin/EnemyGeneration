using Assets.Codebase.ObjectPool;
using Assets.Codebase.ObjectPool.Infrastructure;
using Assets.Codebase.Utils;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _prewarmedEnemiesCount;

    private Pool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new Pool<Enemy>(new MonoBehaviourFactory<Enemy>(_enemyPrefab), _prewarmedEnemiesCount);
    }

    public Enemy SpawnInstance()
    {
        Enemy enemy = _enemyPool.Get();

        enemy.transform.position = transform.position;

        enemy.gameObject.SetActive(true);

        Vector3 randomDirection = Randomizer.GetRandomVector();

        enemy.MoveDirection(new Vector3(randomDirection.x, 0, randomDirection.z));

        return enemy;
    }
}
