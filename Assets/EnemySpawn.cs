using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyPool enemyPool;
    public GameObject door;
    public List<EnemyPool> enemyPools;
    public float spawnInterval = 5.0f;
    public UI_InGame uiInGame;
    public GameObject boss;
    private float time;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }
    void FixedUpdate()
    {
        time = uiInGame.timeRemain;
        if (door != null)
        {
            var bossHeatlh = boss.GetComponent<EnemyStats>();
            if (time >= 902f && bossHeatlh.currentHealth <= 0)
            {
                door.SetActive(true);
            }
        }

        if (time >= 900f)
        {
            enemyPools.RemoveAll(pool => pool == null);
            foreach (var pool in enemyPools)
            {
                pool.gameObject.SetActive(false);
            }
            CancelInvoke(nameof(SpawnEnemy));
            boss.SetActive(true);
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) { CancelInvoke(nameof(SpawnEnemy)); }
    }

    void SpawnEnemy()
    {
        if (enemyPools.Count == 0) return;
        EnemyPool selectedPool = enemyPools[Random.Range(0, enemyPools.Count)];

        GameObject enemy = selectedPool.GetEnemy();
        //GameObject enemy = enemyPool.GetEnemy();
        enemy.transform.position = new Vector3(Random.Range(-40f, 40f), 0, Random.Range(-5f, 5f));
        //if (enemyPool.ActiveEnemyCount() < enemyPool.poolSize)
        //{
        //    GameObject enemy = enemyPool.GetEnemy();
        //    enemy.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        //}
        //else
        //{
        //    Debug.Log("Đã đạt tối đa số lượng quái vật trong pool.");
        //}
    }
}
