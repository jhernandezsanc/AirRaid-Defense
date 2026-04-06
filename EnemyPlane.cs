using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    [Header("Plane Settings")]
    public float moveSpeed = 3f;
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Movement Settings")]
    public bool movingRight = true;
    public float offScreenX = 10f;

    [Header("Bomb Settings")]
    public GameObject bombPrefab;
    public float dropInterval = 2f;
    private float dropTimer;

    [Header("Explosion Settings")]
    public GameObject explosionPrefab;
    public int scoreValue = 100;

    [Header("Audio Settings")]
    public AudioClip hitsound;
    private AudioSource audioSource;

    // Initialize health and audio source
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame : Handle movement and bomb dropping
    void Update()
    {
        MovePlane();
        HandleBombDrop();
    }

    // Handle the movement of the plane : Every time it goes off screen it will notify the spawner and destroy itself and then spawn a new plane
    void MovePlane()
    {
        // Move the plane in the specified direction and check if it goes off-screen to notify the spawner and destroy itself
        if (movingRight)
        {
            // Move right and check if it goes off the right edge of the screen
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (transform.position.x > offScreenX)
            {
                NotifySpawnerAndDestroy(false);
            }
        }
        else
        {
            // Move left and check if it goes off the left edge of the screen
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            if (transform.position.x < -offScreenX)
            {
                NotifySpawnerAndDestroy(false);
            }
        }
    }

    // Handle bomb dropping
    void HandleBombDrop()
    {
        if (bombPrefab == null) return;
        // Increment the drop timer and check if it's time to drop a bomb
        dropTimer += Time.deltaTime;

        if (dropTimer >= dropInterval)
        {
            dropTimer = 0f;
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }

    // Handle taking damage : If health drops to 0 or below, the plane dies and spawns an explosion and adds score
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (audioSource != null && hitsound != null)
        {
            audioSource.PlayOneShot(hitsound);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle the death of the plane : Spawn an explosion, add score, and notify the spawner
    void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position + new Vector3(0f, -1.6f, 0f), Quaternion.identity);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        NotifySpawnerAndDestroy(true);
    }

    // Notify the spawner that the plane has been removed and destroy the plane : The wasShotDown parameter indicates whether the plane was destroyed by the player or went off-screen
    void NotifySpawnerAndDestroy(bool wasShotDown)
    {
        if (Spawner.Instance != null)
        {
            Spawner.Instance.PlaneRemoved(wasShotDown);
        }

        Destroy(gameObject);
    }
}