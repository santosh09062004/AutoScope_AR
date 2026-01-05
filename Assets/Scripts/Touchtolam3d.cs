using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touchtolam3d : MonoBehaviour
{
    void Update()
    {
        // Check for touch input or mouse click
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Load the next scene (by build index)
            SceneManager.LoadScene("Lamborghini 3D");
        }
    }
}
