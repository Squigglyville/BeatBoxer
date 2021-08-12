using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoQueenScript : MonoBehaviour
{
    public GameObject lightBulb;
    public GameObject extraImages;
    
    public void TurnOffLightBulb()
    {
        lightBulb.GetComponent<SpriteRenderer>().enabled = false;
        extraImages.SetActive(false);
    }

    public void TurnOnLightBulb()
    {
        lightBulb.GetComponent<SpriteRenderer>().enabled = true;
        extraImages.SetActive(true);
    }
}
