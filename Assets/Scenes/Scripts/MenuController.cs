using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject firstMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject playMenu;
    public GameObject simplifiedMenu;
    public GameObject advancedMenu;
    public GameObject tutorialMenu;

    private void Start()
    {
        firstMenu.SetActive(true);
        playMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        simplifiedMenu.SetActive(false);
        advancedMenu.SetActive(false);
        tutorialMenu.SetActive(false);
    }
    public void Play()
    {
        firstMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void Tutorial()
    {
        firstMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }

    public void Settings()
    {
        firstMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Credits()
    {
        firstMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void Simplified()
    {
        playMenu.SetActive(false);
        simplifiedMenu.SetActive(true);
    }

    public void Advanced()
    {
        playMenu.SetActive(false);
        advancedMenu.SetActive(true);
    }

    public void BassBusterAdvanced()
    {
        SceneManager.LoadScene("BassBusterAdvanced");
    }

    public void BassBusterSimple()
    {
        SceneManager.LoadScene("BassBusterSimple");
    }

    public void DiscoQueenAdvanced()
    {
        SceneManager.LoadScene("DiscoQueenAdvanced");
    }

    public void DiscoQueenSimple()
    {
        SceneManager.LoadScene("DiscoQueenSimple");
    }

    public void TutorialAdvanced()
    {
        SceneManager.LoadScene("Tutorial1Advanced");
    }

    public void TutorialSimple()
    {
        SceneManager.LoadScene("Tutorial1Simple");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Back()
    {
        Debug.Log("Back");
        firstMenu.SetActive(true);
        playMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        simplifiedMenu.SetActive(false);
        advancedMenu.SetActive(false);
        tutorialMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
