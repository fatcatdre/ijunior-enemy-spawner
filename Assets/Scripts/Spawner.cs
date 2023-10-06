using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _delay = 2f;

    private SpawnPoint[] _spawnPoints;
    private WaitForSeconds _waitTime;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        _waitTime = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (enabled)
        {
            SpawnPoint spawnPoint = GetRandomSpawnPoint();
            Enemy spawnedEnemy = Instantiate(_prefab, spawnPoint.transform.position, Quaternion.identity);

            spawnedEnemy.transform.forward = spawnPoint.MoveDirection;

            yield return _waitTime;
        }
    }

    private SpawnPoint GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];
}
