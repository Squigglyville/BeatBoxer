using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endSplashScript : MonoBehaviour
{
    public Scene winnerCalculatorScene;
    private int currentScene;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextSceneDelay());
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator NextSceneDelay()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene(currentScene + 1);
        yield return null;
    }
}
