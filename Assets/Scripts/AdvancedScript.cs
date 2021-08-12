using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedScript : MonoBehaviour
{
    //Makes scene start in the correct difficulty
    void Start()
    {
        GetComponent<Conductor>().simplifiedOn = false;
    }
}
