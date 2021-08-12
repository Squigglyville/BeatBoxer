using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator animator;
    bool canPunch;
    public static PlayerBehaviour player;
    public bool simplifiedOn;
    
    // Start is called before the first frame update
    void Start()
    {
        canPunch = true;
        player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            if(canPunch == true)
            {
                if(Conductor.instance.playerAttacking == true)
                {
                    animator.SetTrigger("Left");
                    canPunch = false;
                    Conductor.instance.PlayerPunch("Left");
                    if(simplifiedOn == true)
                    {
                        Conductor.instance.leftAnimator.SetTrigger("LeftBlock");
                        Conductor.instance.middleAnimator.SetTrigger("LeftBlock");
                    }


                }
                else
                {
                    if(simplifiedOn == false)
                    {
                        animator.SetTrigger("DodgeLeft");
                        canPunch = false;
                        Conductor.instance.PlayerDodge("Left");
                       
                    }
                    else
                    {
                        animator.SetTrigger("Duck");
                        canPunch = false;
                        Conductor.instance.PlayerDodge("Up");
                    }
                    
                }
                
            }
            
        }

        if (Input.GetButtonDown("Right"))
        {
            if (canPunch == true)
            {
                if (Conductor.instance.playerAttacking == true)
                {
                    animator.SetTrigger("Right");
                    canPunch = false;
                    Conductor.instance.PlayerPunch("Right");
                    if (simplifiedOn == true)
                    {
                        Conductor.instance.rightAnimator.SetTrigger("RightBlock");
                        Conductor.instance.middleAnimator.SetTrigger("RightBlock");
                    }

                }
                else
                {
                    if(simplifiedOn == false)
                    {
                        animator.SetTrigger("DodgeRight");
                        canPunch = false;
                        Conductor.instance.PlayerDodge("Right");
                    }
                    else
                    {
                        animator.SetTrigger("Duck");
                        canPunch = false;
                        Conductor.instance.PlayerDodge("Up");
                    }
                    
                }
                
            }
            
        }

        if (Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
        {
            if (canPunch == true)
            {
                if (Conductor.instance.playerAttacking == true)
                {
                    animator.SetTrigger("Up");
                    canPunch = false;
                    Conductor.instance.PlayerPunch("Up");
                    if (simplifiedOn == true)
                    {
                       
                        Conductor.instance.middleAnimator.SetTrigger("MiddleBlock");
                    }
                }
                else
                {
                    animator.SetTrigger("Duck");
                    canPunch = false;
                    Conductor.instance.PlayerDodge("Up");
                }

            }
            
        }
    }

    public void CanPunch()
    {
        canPunch = true;
    }
}
