using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public int poolSize = 5;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    //private List<GameObject> enemyPool;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        // Khởi tạo pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        foreach (var enemy in activeEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                ResetEenemy(enemy);
                enemy.SetActive(true);
                return enemy;
            }
        }
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            activeEnemies.Add(enemy);
            return enemy;
        }
        else
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(true);
            activeEnemies.Add(enemy);
            return enemy;
        }
    }
    private void ResetEenemy(GameObject enemy)
    {
        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if (stats != null)
        {
            stats.ResetStats();
        }

        enemy.transform.position = Vector3.zero;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
        activeEnemies.Remove(enemy);
    }

    public int ActiveEnemyCount()
    {
        return poolSize - enemyPool.Count;
    }

    public GameObject CheckEnemy()
    {
        foreach (var enemy in activeEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        return null;
    }
}