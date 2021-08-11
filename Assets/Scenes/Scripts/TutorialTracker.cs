using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialTracker : MonoBehaviour
{
    public float numberOfSuccesses;
    public float successesNeededToProgress;
    public int currentScene;

    public Text successText;

    // Update is called once per frame
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if(numberOfSuccesses >= successesNeededToProgress)
        {
            StartCoroutine(NextSceneDelay());
        }

        if(successText != null)
        {
            successText.text = (successesNeededToProgress - numberOfSuccesses).ToString();
        }
    }

    IEnumerator NextSceneDelay()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(currentScene + 1);
        yield return null;
    }
}
