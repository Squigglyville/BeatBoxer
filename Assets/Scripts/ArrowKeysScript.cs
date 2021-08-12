using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeysScript : MonoBehaviour
{
    //Behaviour for arrow keys in the tutorial

    public static ArrowKeysScript arrowKeys;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject centreArrows;

    private void Awake()
    {
        arrowKeys = this;
    }
    
    public void DirectionFlash(string direction)
    {
        if(direction == "Left")
        {
            leftArrow.SetActive(true);
        }
        if (direction == "Right")
        {
            rightArrow.SetActive(true);
        }
        if (direction == "Up")
        {
            centreArrows.SetActive(true);
        }
    }
}
