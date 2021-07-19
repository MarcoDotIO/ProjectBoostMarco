using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Logging quit
            Debug.Log("Quitting Application...");

            // Quitting Application
            Application.Quit();
        }
    }
}
