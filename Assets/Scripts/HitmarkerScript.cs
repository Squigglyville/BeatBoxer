using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerScript : MonoBehaviour
{
    public bool canHitMark;
    
    private void Start()
    {
        canHitMark = true;
    }
    private void OnEnable()
    {
        StartCoroutine(TurnOff());
        
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
        
        canHitMark = true;
        yield return null;
    }
}
