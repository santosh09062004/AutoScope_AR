using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Videoolaplayerstop : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component
        videoPlayer = GetComponent<VideoPlayer>();

        // Subscribe to the loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Method to handle video completion
    void OnVideoFinished(VideoPlayer vp)
    {
        // Load the next scene (use the scene name or build index)
        SceneManager.LoadScene("ola 3d");
    }
}
