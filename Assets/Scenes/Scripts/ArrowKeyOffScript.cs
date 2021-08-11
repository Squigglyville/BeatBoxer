using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowKeyOffScript : MonoBehaviour
{
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
