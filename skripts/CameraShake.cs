using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPos;
    private float shakeCooldown = 0f;

    
    // Initialize the singleton instance and store the original position
    void Awake() {
        Instance = this;
        originalPos = transform.localPosition;
    }
    // Update is called once per frame : Decrease shake cooldown over time to prevent shake spamming
    void Update() {
        if (shakeCooldown > 0)
            shakeCooldown -= Time.deltaTime;
    }
    // Public method to trigger the camera shake with specified duration and magnitude
    public void Shake(float duration, float magnitude) {
        // prevent spam
        if (shakeCooldown > 0) return;

        shakeCooldown = 0.2f; // adjust this to control frequency of shakes
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    // Coroutine to handle the camera shake effect
    IEnumerator ShakeCoroutine(float duration, float magnitude) {
        float elapsed = 0f;
        // Shake the camera by randomly offsetting its position within a circle defined by the magnitude
        while (elapsed < duration) {  
            // Generate random offsets for x and y within the range of -magnitude to +magnitude
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply the random offsets to the camera's original position
            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            // Increment the elapsed time and wait for the next frame
            elapsed += Time.deltaTime;
            yield return null;
        }
        // After shaking, reset the camera to its original position
        transform.localPosition = originalPos;
    }
}