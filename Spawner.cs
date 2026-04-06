using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [Header("Plane Prefabs")]
    public GameObject heavyBomberLeft;
    public GameObject heavyBomberRight;
    public GameObject lightBomberLeft;
    public GameObject lightBomberRight;

    [Header("Spawn Settings")]
    public int maxPlanes = 2;
    public float spawnY = 4f;
    public float leftSpawnX = -9f;
    public float rightSpawnX = 9f;
    public float respawnDelay = 1.5f;

    [Header("Difficulty")]
    public float speedIncreasePerSpawn = 0.2f;
    public float bombRateIncreasePerSpawn = 0.1f;
    public int difficultyLevel = 0;
    // Track the number of active planes to control spawning and difficulty scaling : Limit 2
    private int activePlanes = 0;
    // Initialize the singleton instance and start spawning planes at the beginning of the game
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update : Spawn initial planes at the start of the game
    void Start()
    {
        SpawnRandomPlane();
        SpawnRandomPlane();
    }

    // Called when a plane is removed (either shot down or left the screen)
    public void PlaneRemoved(bool wasShotDown)
    {
        activePlanes = Mathf.Max(0, activePlanes - 1);
        // If the plane was shot down, increase the difficulty level to make the next planes faster and drop bombs more frequently
        if (wasShotDown)
        {
            difficultyLevel++;
        }

        StartCoroutine(RespawnAfterDelay());
    }
    // Coroutine to handle respawning a new plane after a delay, checking if the number of active planes is below the maximum allowed before spawning a new one
    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (activePlanes < maxPlanes)
        {
            SpawnRandomPlane();
        }
    }

    // Method to spawn a random plane from either the left or right side, choosing between heavy and light bombers
    void SpawnRandomPlane()
    {   // Randomly decide whether to spawn from the left or right and whether to use a heavy or light bomber, then instantiate the chosen plane prefab at the appropriate spawn position and apply difficulty scaling to its speed and bomb drop rate
        bool spawnFromLeft = Random.value > 0.5f;
        bool useHeavy = Random.value > 0.5f;

        GameObject prefabToSpawn;
        // Choose the appropriate prefab based on the random choices for spawn side and bomber type
        if (useHeavy)
        {
            prefabToSpawn = spawnFromLeft ? heavyBomberRight : heavyBomberLeft;
        }
        else
        {
            prefabToSpawn = spawnFromLeft ? lightBomberRight : lightBomberLeft;
        }

        // Determine the spawn position based on the chosen side
        // This creates a Vector3 for the spawn position, using the left or right spawn X coordinate and the fixed spawn Y coordinate, with a Z coordinate of 0 for 2D gameplay
        Vector3 spawnPos = spawnFromLeft
            ? new Vector3(leftSpawnX, spawnY, 0f)
            : new Vector3(rightSpawnX, spawnY, 0f);

        GameObject newPlane = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        EnemyPlane plane = newPlane.GetComponent<EnemyPlane>();
        if (plane != null)
        {
            // Apply difficulty scaling to the plane's speed and bomb drop rate
            plane.moveSpeed += difficultyLevel * speedIncreasePerSpawn;
            plane.dropInterval = Mathf.Max(0.4f, plane.dropInterval - difficultyLevel * bombRateIncreasePerSpawn);
        }

        activePlanes++; // Increment the count of active planes
    }
}