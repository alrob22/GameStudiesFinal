using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;

    public Rigidbody2D rb;
    public Rigidbody2D playerRb;
    Vector2 movement;

    void Start() {

        if (PlayerStats.killed.Contains(gameObject.name)) {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // have the enemy chase the player
        if (playerRb.position.x < rb.position.x) {
            movement.x = -1;
        } else if (playerRb.position.x > rb.position.x) {
            movement.x = 1;
        }
        if (playerRb.position.y < rb.position.y) {
            movement.y = -1;
        } else if (playerRb.position.y > rb.position.y) {
            movement.y = 1;
        }

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
