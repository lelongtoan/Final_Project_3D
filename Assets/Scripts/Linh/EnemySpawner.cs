using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public Transform[] spawnPoints; 
    public int maxEnemies = 10; 
    public float respawnDelay = 5f;

    private GameObject[] spawnedEnemies; 
    private int enemyID = 0; 

    void Start()
    {
        spawnedEnemies = new GameObject[spawnPoints.Length];
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                // Nếu điểm spawn này chưa có quái hoặc quái đã bị phá hủy
                if (spawnedEnemies[i] == null)
                {
                    if (CountActiveEnemies() < maxEnemies)
                    {
                        SpawnEnemyAtPoint(i);
                    }
                }
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }

    void SpawnEnemyAtPoint(int index)
    {
        if (enemyPrefabs.Count == 0 || spawnPoints.Length == 0 || index < 0 || index >= spawnPoints.Length)
        {
            Debug.LogWarning("Invalid spawn point or no enemy prefabs defined!");
            return;
        }

        Transform spawnPoint = spawnPoints[index];
        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        GameObject enemy = Instantiate(randomEnemy, spawnPoint.position, spawnPoint.rotation);

        if (enemy != null)
        {
            AssignEnemyID(enemy);
            spawnedEnemies[index] = enemy; // Lưu tham chiếu quái vào mảng
            Debug.Log($"Enemy spawned at: {spawnPoint.position} with ID: {enemyID - 1}");
        }
    }

    void AssignEnemyID(GameObject enemy)
    {
        EnemyInfor enemyScript = enemy.GetComponent<EnemyInfor>();
        if (enemyScript != null)
        {
            enemyScript.enemyId = enemyID++;
        }
    }

    int CountActiveEnemies()
    {
        int count = 0;
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                count++;
            }
        }
        return count;
    }
}