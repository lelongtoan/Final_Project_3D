using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; 
    public Transform[] spawnPoints; 
    public List<GameObject> activeEnemies = new List<GameObject>(); 
    public int maxEnemies = 10;  
    public float respawnDelay = 5f; 
    public float minSpawnDistance = 3f; 

    private int enemyID = 0;  

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            CleanActiveEnemies();  

            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();  
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }

    void CleanActiveEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null); 
        Debug.Log("Cleaned missing enemies. Active count: " + activeEnemies.Count);
    }

    // Hàm spawn quái
    void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No enemies or spawn points defined!");
            return;
        }

        Transform spawnPoint = GetValidSpawnPoint();
        if (spawnPoint == null)
        {
            Debug.LogWarning("No valid spawn points available.");
            return;
        }

        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        GameObject enemy = Instantiate(randomEnemy, spawnPoint.position, spawnPoint.rotation);
        if (enemy != null)
        {
            // Gán ID cho quái vừa spawn
            EnemyInfor enemyScript = enemy.GetComponent<EnemyInfor>();
            if (enemyScript != null)
            {
                enemyScript.enemyId = enemyID;
                enemyID++; 
            }

            activeEnemies.Add(enemy); 
            Debug.Log("Enemy spawned at: " + spawnPoint.position + " with ID: " + enemyID);
        }
    }

    Transform GetValidSpawnPoint()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            bool isValid = true;

            foreach (var enemy in activeEnemies)
            {
                if (Vector3.Distance(spawnPoint.position, enemy.transform.position) < minSpawnDistance)
                {
                    isValid = false;  
                    break;
                }
            }

            if (isValid)
            {
                return spawnPoint;
            }
        }

        return null;
    }
}