using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    //Animation for the stars in the main menu

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject star5;
    public GameObject star6;

    // Start is called before the first frame update
    void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        star4.SetActive(false);
        star5.SetActive(false);
        star6.SetActive(false);
        StartCoroutine(StarDelay());
    }

    IEnumerator StarDelay()
    {
        star1.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        star2.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        star3.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        star4.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        star5.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        star6.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        yield return null;
    }
}
