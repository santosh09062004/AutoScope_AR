using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touchtorst3d : MonoBehaviour
{
    void Update()
    {
        // Check for touch input or mouse click
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Load the next scene (by build index)
            SceneManager.LoadScene("rst 3d");
        }
    }
}
