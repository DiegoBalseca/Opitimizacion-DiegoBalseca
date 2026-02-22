using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _xRange = 5f;
    [SerializeField] private float _zSpawn = 10f; // altura de spawn

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, _spawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-_xRange, _xRange), 0, _zSpawn);
        GameObject enemy = PoolManager.Instance.GetPooledObject("Enemigos", spawnPos, Quaternion.identity);

        if (enemy != null)
        {
            enemy.SetActive(true);
        }
    }
}