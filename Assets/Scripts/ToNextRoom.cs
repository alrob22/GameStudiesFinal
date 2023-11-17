using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextRoom : MonoBehaviour
{
    [SerializeField] string room;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Application.LoadLevel(room);
            Debug.Log("To next room!");
        }
    }
}
