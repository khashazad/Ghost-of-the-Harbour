using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign enemy prefab in the Inspector
    public int enemyCount = 10; // Number of enemies to spawn
    public float spawnRadius = 10f; // Adjust spawn area size

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPoint.y = 0; // Ensure it spawns on the ground
        return randomPoint;
    }
}
