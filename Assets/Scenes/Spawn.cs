using UnityEngine;
using System.Collections;
using Unity.XR.CoreUtils;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float interval = 12f;

    private float MinSpawnRadius = 10f;

    private float timer = 0f;
    private int spawnCount = 1;
    private Transform target;

    void Start()
    {
        Debug.Log("Spawn system started");

        // Find XROrigin and set it as target
        XROrigin xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin != null)
        {
            target = xrOrigin.transform;
            Debug.Log("Target set to XROrigin");
        }
        else
        {
            Debug.LogWarning("XROrigin not found!");
        }

        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Every 30 seconds, double the spawn count
        if (timer >= 30f)
        {
            spawnCount *= 2;
            Debug.Log($"Spawn count doubled! Now spawning {spawnCount} enemies per interval.");
            timer = 0f;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Check if target exists and is within radius
            float distance = Vector3.Distance(transform.position, target.position);

            if (target != null && distance >= MinSpawnRadius)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("Target is out of range, skipping spawn.");
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
