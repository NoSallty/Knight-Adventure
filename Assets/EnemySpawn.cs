using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<EnemyPool> enemyPools;
    public float spawnInterval = 2.0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPools.Count == 0) return;

        EnemyPool selectedPool = enemyPools[Random.Range(0, enemyPools.Count)];
        GameObject enemy = selectedPool.GetEnemy();
        enemy.transform.position = new Vector3(Random.Range(-40f, 40f), 0, Random.Range(-40f, 40f));
    }
}
