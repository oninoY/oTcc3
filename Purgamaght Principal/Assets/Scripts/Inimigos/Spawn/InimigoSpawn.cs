using UnityEngine;

public class InimigoSpawn : MonoBehaviour
{
    public GameObject enemyPrefab1; // Assign this in the Inspector
    public GameObject enemyPrefab2; // Assign this in the Inspector
    public float spawnDelay = 2f; // Delay before spawning

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay); // Spawn enemies repeatedly
    }

    private void SpawnEnemy()
    {
        // Randomly decide to spawn enemyPrefab1, enemyPrefab2, or nothing
        int randomValue = Random.Range(0, 3); // 0, 1, or 2
        GameObject enemyToSpawn = null;

        if (randomValue == 0)
        {
            enemyToSpawn = enemyPrefab1;
        }
        else if (randomValue == 1)
        {
            enemyToSpawn = enemyPrefab2;
        }
        // If randomValue == 2, do not spawn anything

        if (enemyToSpawn != null)
        {
            // Spawn the enemy at the position of the GameObject that has this script
            GameObject enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            enemy.SetActive(true);
            Debug.Log("Spawned enemy: " + enemyToSpawn.name);
        }
        else
        {
            Debug.Log("No enemy spawned.");
        }

        // Optionally, if you want to destroy this spawner after one use, uncomment the line below
         Destroy(gameObject); // This will destroy the GameObject that has this script
    }
}