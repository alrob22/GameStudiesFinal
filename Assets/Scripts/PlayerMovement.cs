using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // boundary checks for the screen
        // left
        if (rb.position.x <= -11) {
            movement.x++;
        }
        // right
        if (rb.position.x >= 11) {
            movement.x--;
        }
        // up
        if (rb.position.y <= 4.2) {
            movement.y++;
        }
        // down
        if (rb.position.y >= -4.2) {
            movement.y--;
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}