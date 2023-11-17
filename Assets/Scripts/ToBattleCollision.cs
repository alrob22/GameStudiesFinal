using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBattleCollision : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Application.LoadLevel("battle");
            Debug.Log("its Colliding!");
        }
    }
}
