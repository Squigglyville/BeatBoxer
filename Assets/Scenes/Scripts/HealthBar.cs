using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image filledPart;
    public Image background;

    public void ShowHealthFraction(float fraction)
    {
        filledPart.rectTransform.localScale = new Vector3(1, fraction, 1);
        if (fraction < 1)
        {
            filledPart.enabled = true;
            background.enabled = true;
        }
        
    }
}
