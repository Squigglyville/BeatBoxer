using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameObject[] array;
    public List<GameObject> list;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeToBlack(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeIn()
    {
        StartCoroutine(FadeToBlack());
    }

    public IEnumerator FadeToBlack(bool fadeToBlack = true, float fadeSpeed = 0.3f)
    {
        Color objectcolor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while(blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectcolor.a + (fadeSpeed * Time.deltaTime);

                objectcolor = new Color(objectcolor.r, objectcolor.g, objectcolor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectcolor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectcolor.a - (fadeSpeed * Time.deltaTime);

                objectcolor = new Color(objectcolor.r, objectcolor.g, objectcolor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectcolor;
                yield return null;
            }
        }
        
    }
}
