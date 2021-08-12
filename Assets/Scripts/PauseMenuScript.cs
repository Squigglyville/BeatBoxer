using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    bool paused;
    void Start()
    {
        paused = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(paused == false)
            {
                Conductor.instance.musicSource.Pause();
                Conductor.instance.leftAnimator.speed = 0;
                Conductor.instance.rightAnimator.speed = 0;
                Conductor.instance.middleAnimator.speed = 0;
                pauseMenu.SetActive(true);
                paused = true;
                Conductor.instance.paused = true;
            }
            else
            {
                Conductor.instance.musicSource.Play();
                Conductor.instance.leftAnimator.speed = 1;
                Conductor.instance.rightAnimator.speed = 1;
                Conductor.instance.middleAnimator.speed = 1;
                pauseMenu.SetActive(false);
                paused = false;
                Conductor.instance.paused = false;
            }
        }
    }

    public void Continue()
    {
        Conductor.instance.musicSource.Play();
        Conductor.instance.leftAnimator.speed = 1;
        Conductor.instance.rightAnimator.speed = 1;
        Conductor.instance.middleAnimator.speed = 1;
        pauseMenu.SetActive(false);
        paused = false;
        Conductor.instance.paused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Quit");
    }
}
