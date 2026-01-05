using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void Quite()
    {
        // Quits the application
        Application.Quit();

        // Debug message for testing in the Unity Editor (since Application.Quit doesn't work in Editor)
        Debug.Log("Game is exiting");
    }
}
