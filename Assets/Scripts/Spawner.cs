using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _timeBetweenSpawns = 2f;

    private SpawnPoint[] _spawnPoints;
    private float _nextSpawnTime;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }

    private void Update()
    {
        TrySpawnEnemy();
    }

    private void TrySpawnEnemy()
    {
        if (Time.time < _nextSpawnTime)
            return;

        SpawnEnemy();

        _nextSpawnTime = Time.time + _timeBetweenSpawns;
    }

    private void SpawnEnemy()
    {
        SpawnPoint spawnPoint = GetRandomSpawnPoint();
        Enemy spawnedEnemy = Instantiate(_enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

        spawnedEnemy.transform.forward = spawnPoint.MoveDirection;
    }

    private SpawnPoint GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];
}
