using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Bounds")]
    public float minX = -7f;
    public float maxX = 7f;

    private Rigidbody2D rb;
    // Initialize the Rigidbody2D component for controlling the turret's movement
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame : Handle player input and move the turret accordingly, while ensuring it stays within the defined horizontal bounds
    void Update() {
        float moveInput = Input.GetAxis("Horizontal"); 
        // Arrow keys OR A/D

        Vector2 velocity = new Vector2(moveInput * moveSpeed, 0f);
        rb.linearVelocity = velocity;

        ClampPosition();
    }

    // Clamp the turret's position within the defined horizontal bounds
    void ClampPosition() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}
