using UnityEngine;

public class Bomb : MonoBehaviour {
    [Header("Movement")]
    public float fallSpeed = 4f;
    public float bottomBound = -5.5f;

    [Header("Damage")]
    public int damage = 20;

    [Header("Explosion")]
    public GameObject explosionPrefab;

    public int scoreValue = 25;
    // Update is called once per frame : Move bomb downwards and check if it hits the base
    void Update() {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        // Check if bomb hits the base (floor)
        if (transform.position.y < bottomBound) {
            HitBase();
        }
    }
    // called by bullet when hit : Will Destroy the bomb, add score and spawn explosion
    public void DestroyBomb() {
        if (GameManager.Instance != null) {
            GameManager.Instance.AddScore(scoreValue);
        }
        if (explosionPrefab != null) {
            // Spawn explosion with a minor offset to align with the bomb's center
            Instantiate(explosionPrefab, transform.position + new Vector3(0f, -1.6f, 0f), Quaternion.identity);
        }
        Destroy(gameObject);
        //Debug.Log("Bomb destroyed by bullet");
    }

    // called when bomb hits the base(floor) : Will damage the base, shake camera, flash screen and spawn explosion 
    void HitBase() {
        if (GameManager.Instance != null) {
            GameManager.Instance.DamageBase(damage);
        }
        if (CameraShake.Instance != null) {
            // Shake the camera with a short duration and moderate intensity
            CameraShake.Instance.Shake(0.2f, 0.15f);
        }
        if (ScreenFlash.Instance != null) {
            // Flash the screen
            ScreenFlash.Instance.Flash();
        }
        if (explosionPrefab != null) {
            // Spawn explosion at the bomb's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}