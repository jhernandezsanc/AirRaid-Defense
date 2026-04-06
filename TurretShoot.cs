using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 12f;
    public float fireRate = 0.25f;
    public int bulletScreenLimit = 10;

    private float fireTimer;
    public AudioClip shootSound;
    private AudioSource audioSource;
    // Initialize the audio source for playing the shooting sound effect
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame : Handle player input for shooting and manage bullet cleanup for bullets that go off-screen
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Shoot();
        }
        // Destroy bullets that go off-screen
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet.transform.position.y > Camera.main.orthographicSize + 1f)
            {
                Destroy(bullet);
            }
        }
    }

    // Instantiate a bullet, set its velocity, and play the shooting sound effect
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * bulletSpeed;
        }
    }
}