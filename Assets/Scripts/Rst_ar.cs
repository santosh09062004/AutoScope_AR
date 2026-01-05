using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rst_ar : MonoBehaviour
{
    public void ChangeTorstarWithDelay()
    {
        StartCoroutine(DelayedSceneChange());
    }

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 1 second
        SceneManager.LoadScene("rst ar");
    }

}
