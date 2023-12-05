using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public Sprite down;
    public Sprite up;
    public Sprite right;
    public Sprite left;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("down")) {
            Debug.Log("down was pressed");
            spriteRenderer.sprite = down;
        }
        if (Input.GetKeyDown("up")) {
            Debug.Log("up was pressed");
            spriteRenderer.sprite = up;
        }
        if (Input.GetKeyDown("right")) {
            Debug.Log("right was pressed");
            spriteRenderer.sprite = right;
        }
        if (Input.GetKeyDown("left")) {
            Debug.Log("left was pressed");
            spriteRenderer.sprite = left;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("collisionMap")) {
            Debug.Log("collision map hit");

            if (Input.GetKeyDown("down")) {
                movement.y--;
            }
            if (Input.GetKeyDown("up")) {
                movement.y++;
            }
            if (Input.GetKeyDown("right")) {
                movement.x--;
            }
            if (Input.GetKeyDown("left")) {
                movement.x++;
            }
        }
    }
}