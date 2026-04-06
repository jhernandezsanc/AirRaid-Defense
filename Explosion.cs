using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifeTime = 0.5f;
    public AudioClip explosionSound;
    private AudioSource audioSource;
    // Initialize the audio source and play the explosion sound, then destroy the explosion object after its lifetime
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
        Destroy(gameObject, lifeTime);
    }
}