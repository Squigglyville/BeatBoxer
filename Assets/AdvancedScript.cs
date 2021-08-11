using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Conductor>().simplifiedOn = false;
    }
}
