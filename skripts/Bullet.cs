using UnityEngine;

public class Bullet : MonoBehaviour {
    public float lifeTime = 3f;

    [Header("Bounds")]
    public float topBound = 6f;

    [Header("Damage")]
    public int damage = 1;

    void Start() {
        Destroy(gameObject, lifeTime); // backup safety
    }

    void Update() {
        // Destroy if it goes off the top of the screen : stops dummy bullets from lingering forever if they miss
        if (transform.position.y > topBound) {
            Destroy(gameObject);
        }
    }

    // Check for collisions with EnemyPlane and Bomb
    void OnTriggerEnter2D(Collider2D other) { 
        // Check if the bullet hit an enemy plane
        EnemyPlane enemy = other.GetComponent<EnemyPlane>();
        // Check if the bullet hit a bomb
        Bomb bomb = other.GetComponent<Bomb>();

        // If it hit an enemy plane, damage it and destroy the bullet
        if (enemy != null) {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
        // If it hit a bomb, destroy the bomb and the bullet
        if (bomb != null) {
            bomb.DestroyBomb();
            Destroy(gameObject);
            return;
        }
    }
}
