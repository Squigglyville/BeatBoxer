using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplfiedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Conductor>().simplifiedOn = true;
    }

    
}
