using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Optionmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeTooptionmenuWithDelay()
    {
        StartCoroutine(DelayedSceneChange());
    }

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 1 second
        SceneManager.LoadScene("Optionmenu");
    }
}
