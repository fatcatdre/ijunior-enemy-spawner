using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _delay = 2f;

    private SpawnPoint[] _spawnPoints;
    private WaitForSeconds _spawnDelay;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();

        UpdateSpawnDelay();
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    private void OnValidate()
    {
        UpdateSpawnDelay();
    }

    private IEnumerator SpawnEnemy()
    {
        while (enabled)
        {
            SpawnPoint spawnPoint = GetRandomSpawnPoint();
            Enemy spawnedEnemy = Instantiate(_prefab, spawnPoint.transform.position, Quaternion.identity);

            spawnedEnemy.transform.forward = spawnPoint.MoveDirection;

            yield return _spawnDelay;
        }
    }

    private void UpdateSpawnDelay()
    {
        _spawnDelay = new WaitForSeconds(_delay);
    }

    private SpawnPoint GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];
}
