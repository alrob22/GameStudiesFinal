using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    bool start = false;

    // Update is called once per frame
    void Update()
    {
        start = Input.GetButtonDown("Submit");
        if (start) {
            //Application.LoadLevel("Room1");
            SceneManager.LoadScene("Room1");
        }
    }
}
