using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowKeyOffScript : MonoBehaviour
{
    //Allows arrow keys to turn off again after being activated
    private void OnEnable()
    {
        StartCoroutine(TurnOff());

    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);
        gameObject.GetComponent<Image>().color = Color.gray;
        
        yield return null;
    }
}
