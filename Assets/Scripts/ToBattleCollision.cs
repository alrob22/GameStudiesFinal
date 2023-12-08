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
            GameObject a = GameObject.FindGameObjectWithTag("Player");

            PlayerStats.x = a.transform.position.x;
            PlayerStats.y = a.transform.position.y;
            Debug.Log(PlayerStats.x);

            PlayerStats.killed[PlayerStats.index] = gameObject.name;
            PlayerStats.index++;

            SceneManager.LoadScene("battle");
            Debug.Log("To battle!");
        }
    }
}
