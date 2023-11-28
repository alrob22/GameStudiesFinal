using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ToBattleCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            //Application.LoadLevel("battle");
            SceneManager.LoadScene("battle");
            Debug.Log("To battle!");
        }
    }
}
