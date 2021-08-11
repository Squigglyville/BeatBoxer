using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNumberAdder : MonoBehaviour
{
    // Start is called before the first frame update
    public float beatsToDodgeFactor;
    public float beatsToDodgeAdjustment;
    public float beatsToHitFactor;
    public float beatsToHitAdjustment;

    public bool doublePunching;

    void Awake()
    {
        for (int i = 3; i < 500; i++)
        {
                       
            if ((i % beatsToDodgeFactor) == 0)
            {
                Conductor.instance.beatsToDodge.Add(i + beatsToDodgeAdjustment);
                Conductor.instance.beatsToFlash.Add(i + beatsToDodgeAdjustment - 1);
                if(doublePunching == true)
                {
                    Conductor.instance.beatsToDodge.Add(i + beatsToDodgeAdjustment + 0.5f);
                    Conductor.instance.beatsToFlash.Add(i + beatsToDodgeAdjustment - 1 + 0.5f);
                }
            }
           
        }
        for (int i = 3; i < 500; i++)
        {

            if ((i % beatsToHitFactor) == 0)
            {
                Conductor.instance.beatsToHit.Add(i + beatsToHitAdjustment);
                Conductor.instance.beatsWhereHitMarkerActivates.Add(i + beatsToHitAdjustment - 1);
                if (doublePunching == true)
                {
                    Conductor.instance.beatsToHit.Add(i + beatsToHitAdjustment + 0.5f);
                    Conductor.instance.beatsWhereHitMarkerActivates.Add(i + beatsToHitAdjustment - 1 + 0.5f);
                }
            }

        }

        if(doublePunching == true)
        {
            for (int i = 2; i < 500; i++)
            if ((i % 4) == 0)
            {
                Conductor.instance.beatsForDoublePunch.Add(i - 2);
                Conductor.instance.beatsForDoublePunch.Add(i - 1.5f);

            }
        }

        for (int i = 0; i < 500; i++)
        {

            if ((i % 3) == 0)
            {
                Conductor.instance.beatDodgeDirections.Add("Left");
                Conductor.instance.beatDirectionsToFlash.Add("Left");
                Conductor.instance.beatHitDirections.Add("Right");
                Conductor.instance.directionsForHitmarker.Add("Right");
            }
            if ((i % 3) == 1)
            {
                Conductor.instance.beatDodgeDirections.Add("Right");
                Conductor.instance.beatDirectionsToFlash.Add("Right");
                Conductor.instance.beatHitDirections.Add("Left");
                Conductor.instance.directionsForHitmarker.Add("Left");
            }
            if ((i % 3) == 2)
            {
                Conductor.instance.beatDodgeDirections.Add("Up");
                Conductor.instance.beatDirectionsToFlash.Add("Up");
                Conductor.instance.beatHitDirections.Add("Up");
                Conductor.instance.directionsForHitmarker.Add("Up");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
