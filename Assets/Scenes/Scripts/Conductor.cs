using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    
    //The beat position when the player inputs
    public float playerBeatInput;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;
    

    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    //Conductor instance
    public static Conductor instance;

    
    //When the player punches variables
    public List<float> beatsToHit;              //List of the songPositionsInBeats the player has to punch on
    public int beatsToHitIndex;                 //Index to move through the list
    public float currentBeatToHit;              //The next beat the player should punch on
    public List<string> beatHitDirections;      //List of direction the punch should go
    public string currentBeatDirectionToHit;    //The next direction the player should punch in

    //When the player dodges variables
    public List<float> beatsToDodge;            //List of the songPositionsInBeats the player has to dodge on
    public int beatsToDodgeIndex;               //Index to move through the list
    public float currentBeatToDodge;            //The next beat the player should dodge on
    public List<string> beatDodgeDirections;    //The direction the dodge should go
    public string currentBeatDirectionToDodge;  //The next direction the player should dodge in

    public List<float> beatsForDoublePunch;
    public int doublePunchIndex;
    public float nextDoublePunch;

    //When the lightbulb flashes variables
    public List<float> beatsToFlash;            //List of the songPositionsInBeats the lightbulb flashes on
    public List<string> beatDirectionsToFlash;
    public string currentFlashDirection;
    public int flashDirectionIndex;
    public int beatsToFlashIndex;               //Index to move through the list
    public float currentBeatToFlash;            //The next beat the player should dodge on
    public Animator leftlightbulb;
    public Animator rightlightbulb;
    public Animator centrelightbulb;

    //Changing phases between attacking and dodging
    public List<float> beatsWherePhaseChanges;          //List of beats where the phases changes, usually every 4 beats
    public int phaseChangeIndex;                        //Index to move through list
    public float nextPhaseChange;                       //The next beat the phase changes on

    //Hitmarkers
    public List<float> beatsWhereHitMarkerActivates;
    public List<string> directionsForHitmarker;
    public int hitmarkerIndex;
    public int hitmarkerDirectionIndex;
    public float nextHitmarker;
    public string nextHitmarkerDirection;

    //HeadBobbing
    public List<float> beatsWhereHeadBobs;
    public int headBobIndex;
    public float nextHeadBob;
    bool headUp;

    //When the player is attacking and when they are dodging
    public bool playerAttacking;
      
    //Prevents rapid attacks
    public bool ableToHit;
    bool opponentAbleToHit;

    public ParticleSystem playerParticles;
    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;
    public ParticleSystem middleParticles;

    //Training bot variables
    public Animator middleAnimator;
    public Animator leftAnimator;
    public Animator rightAnimator;
    public GameObject hitMarkerLeft;
    public GameObject hitMarkerUp;
    public GameObject hitMarkerRight;
    public bool canHitMark;


    //dealing damage to player
    public float lowerTimeToDodge;
    public float upperTimeToDodge;
    public bool dodged;
    public HealthSystem playerHealthSystem;


    //dealing damage to opponent
    public HealthSystem opponentHealthSystem;

    public bool tutorialOn;
    public TutorialTracker tutorialTracker;

    public GameObject endGameSplash;
    public GameObject KOSplash;
    public float beatsWhenGameEnds;
    bool instantiated;

    public bool simplifiedOn;

    public bool paused;
    public float timePaused;
    public GameObject fadeToBlack;
    void Awake()
    {
        instance = this;
        for (int i = 0; i < 150; i++)
        {
            if(tutorialOn == true)
            {
                if ((i % 1) == 0)
                {
                    beatsWhereHeadBobs.Add(i + 0.5f);
                }
            }
            else
            {
                if ((i % 2) != 0)
                {
                    beatsWhereHeadBobs.Add(i);
                }
            }
            
        }
    }

    void Start()
    {
        PlayerPrefs.DeleteAll();

        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();

        
            //Make a list for every 1st beat of the bar
            for (int i = 0; i < 150; i++)
            {
                if ((i % 4) == 0)
                {
                    beatsWherePhaseChanges.Add(i);
                }
            }
        
        

        //Setting up beats to hit
        beatsToHitIndex = 0;
        currentBeatToHit = beatsToHit[beatsToHitIndex];
        currentBeatDirectionToHit = beatHitDirections[beatsToHitIndex];
        ableToHit = true;

        if(beatsForDoublePunch.Count > 0)
        {
            doublePunchIndex = 0;
            nextDoublePunch = beatsForDoublePunch[doublePunchIndex];
        }
        

        //Setting up beats to dodge
        beatsToDodgeIndex = 0;
        currentBeatToDodge = beatsToDodge[beatsToDodgeIndex];
        lowerTimeToDodge = currentBeatToDodge - 0.1f;
        upperTimeToDodge = currentBeatToDodge + 0.2f;
        currentBeatDirectionToDodge = beatDodgeDirections[beatsToDodgeIndex];

        //Setting up beats to flash
        beatsToFlashIndex = 0;
        currentBeatToFlash = beatsToFlash[beatsToFlashIndex];
        currentFlashDirection = beatDirectionsToFlash[beatsToFlashIndex];
        
        //Setting up phase changes
        phaseChangeIndex = 0;
        nextPhaseChange = beatsWherePhaseChanges[phaseChangeIndex];
        
        

        //Setting up head bobs
        headBobIndex = 0;
        nextHeadBob = beatsWhereHeadBobs[headBobIndex];

        //setting up hitmarkers
        hitmarkerIndex = 0;
        nextHitmarker = beatsWhereHitMarkerActivates[hitmarkerIndex];
        hitmarkerDirectionIndex = 0;
        nextHitmarkerDirection = directionsForHitmarker[hitmarkerDirectionIndex];

        opponentAbleToHit = true;
        canHitMark = true;

        tutorialTracker.GetComponent<TutorialTracker>();

        timePaused = 0;
    }

    
    void Update()
    {
        if(paused == true)
        {
            timePaused += Time.deltaTime;
        }

        if (paused == false)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset - timePaused);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            //calculate the loop position
            if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
                completedLoops++;
            loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

            loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;

            if (songPositionInBeats >= beatsWhenGameEnds)
            {
                if (instantiated == false)
                {
                    if (middleAnimator.GetComponent<HealthSystem>().currentHealth == 0)
                    {
                        Instantiate(KOSplash);
                        instantiated = true;
                        fadeToBlack.GetComponent<FadeInScript>().FadeIn();
                    }
                    else
                    {
                        Instantiate(endGameSplash);
                        instantiated = true;
                        fadeToBlack.GetComponent<FadeInScript>().FadeIn();
                    }

                }

            }

            //Alternate between attacking and dodging every 4 bars
            if (songPositionInBeats >= (nextPhaseChange))
            {
                if (playerAttacking == false)
                {
                    playerAttacking = true;
                    middleAnimator.SetBool("Blue", true);
                    leftAnimator.SetBool("Blue", true);
                    rightAnimator.SetBool("Blue", true);

                }
                else
                {
                    playerAttacking = false;
                    middleAnimator.SetBool("Blue", false);
                    leftAnimator.SetBool("Blue", false);
                    rightAnimator.SetBool("Blue", false);

                }

                phaseChangeIndex += 1;
                nextPhaseChange = beatsWherePhaseChanges[phaseChangeIndex];

            }
            //If song goes past the next beat to hit, move onto the next beat to hit
            if (songPositionInBeats >= currentBeatToHit + 0.3f)
            {
                beatsToHitIndex += 1;
                currentBeatToHit = beatsToHit[beatsToHitIndex];
                currentBeatDirectionToHit = beatHitDirections[beatsToHitIndex];
                ableToHit = true;
            }

            if (songPositionInBeats >= currentBeatToHit)
            {
                if (ArrowKeysScript.arrowKeys != null)
                {
                    if (currentBeatDirectionToHit == "Left")
                    {
                        ArrowKeysScript.arrowKeys.leftArrow.GetComponent<Image>().color = Color.green;
                    }
                    if (currentBeatDirectionToHit == "Right")
                    {
                        ArrowKeysScript.arrowKeys.rightArrow.GetComponent<Image>().color = Color.green;
                    }
                    if (currentBeatDirectionToHit == "Up")
                    {
                        ArrowKeysScript.arrowKeys.centreArrows.GetComponent<Image>().color = Color.green;
                    }



                }
            }

            //If song goes past the next beat to dodge, move onto the next beat to dodge
            if (songPositionInBeats >= currentBeatToDodge + 0.5f)
            {
                beatsToDodgeIndex += 1;
                currentBeatToDodge = beatsToDodge[beatsToDodgeIndex];
                lowerTimeToDodge = currentBeatToDodge - 0.1f;
                upperTimeToDodge = currentBeatToDodge + 0.3f;
                dodged = false;
                currentBeatDirectionToDodge = beatDodgeDirections[beatsToDodgeIndex];
                ableToHit = true;
                opponentAbleToHit = true;
            }

            //If song goes past the next beat to flash, move onto the next beat to flash
            if (songPositionInBeats >= currentBeatToFlash + 0.3f)
            {
                beatsToFlashIndex += 1;
                currentBeatToFlash = beatsToFlash[beatsToFlashIndex];
                currentFlashDirection = beatDirectionsToFlash[flashDirectionIndex];
            }

            //Animate the opponent at the right time
            if (songPositionInBeats >= currentBeatToDodge)
            {
                if (opponentAbleToHit == true)
                {
                    if (currentBeatDirectionToDodge == "Left")
                    {
                        rightAnimator.SetTrigger("RightPunch");
                        opponentAbleToHit = false;
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/Punch");
                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.leftArrow.GetComponent<Image>().color = Color.green;
                        }
                    }
                    if (currentBeatDirectionToDodge == "Right")
                    {
                        leftAnimator.SetTrigger("LeftPunch");

                        opponentAbleToHit = false;
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/Punch");
                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.rightArrow.GetComponent<Image>().color = Color.green;
                        }
                    }
                    if (currentBeatDirectionToDodge == "Up")
                    {
                        middleAnimator.SetTrigger("MiddlePunch");

                        opponentAbleToHit = false;
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/Punch");
                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.centreArrows.GetComponent<Image>().color = Color.green;
                        }
                    }

                }

            }

            if (beatsForDoublePunch.Count > 0)
            {
                if (songPositionInBeats >= nextDoublePunch + 0.05)
                {
                    doublePunchIndex += 1;
                    nextDoublePunch = beatsForDoublePunch[doublePunchIndex];
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/DoublePunch");
                }
            }


            if (songPositionInBeats >= (nextHitmarker) + 0.3f)
            {
                hitmarkerIndex += 1;
                nextHitmarker = beatsWhereHitMarkerActivates[hitmarkerIndex];
                hitmarkerDirectionIndex += 1;
                nextHitmarkerDirection = directionsForHitmarker[hitmarkerDirectionIndex];
            }


            if (songPositionInBeats >= (nextHitmarker))
            {

                if (nextHitmarkerDirection == "Left")
                {
                    if (hitMarkerLeft.GetComponent<HitmarkerScript>().canHitMark == true)
                    {
                        hitMarkerLeft.SetActive(true);
                        hitMarkerLeft.GetComponent<HitmarkerScript>().canHitMark = false;

                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.DirectionFlash("Left");
                        }
                    }

                }
                if (nextHitmarkerDirection == "Up")
                {
                    if (hitMarkerUp.GetComponent<HitmarkerScript>().canHitMark == true)
                    {
                        hitMarkerUp.SetActive(true);
                        hitMarkerUp.GetComponent<HitmarkerScript>().canHitMark = false;

                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.DirectionFlash("Up");
                        }
                    }

                }
                if (nextHitmarkerDirection == "Right")
                {
                    if (hitMarkerRight.GetComponent<HitmarkerScript>().canHitMark == true)
                    {
                        hitMarkerRight.SetActive(true);
                        hitMarkerRight.GetComponent<HitmarkerScript>().canHitMark = false;

                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.DirectionFlash("Right");
                        }
                    }

                }




            }

            //Animate the lightbulb flash at the right time
            if (songPositionInBeats >= currentBeatToFlash)
            {
                if (leftlightbulb.GetComponent<LightbulbScript>().canFlash == true)
                {
                    if (currentFlashDirection == "Right")
                    {
                        leftlightbulb.SetTrigger("Flash");
                        leftlightbulb.GetComponent<LightbulbScript>().canFlash = false;
                        leftAnimator.SetTrigger("LeftBackswing");
                        flashDirectionIndex += 1;
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/PreparePunch");
                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.DirectionFlash("Right");
                        }
                    }
                }
                if (rightlightbulb.GetComponent<LightbulbScript>().canFlash == true)
                {
                    if (currentFlashDirection == "Left")
                    {
                        rightlightbulb.SetTrigger("Flash");
                        rightlightbulb.GetComponent<LightbulbScript>().canFlash = false;
                        rightAnimator.SetTrigger("RightBackswing");
                        flashDirectionIndex += 1;
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/PreparePunch");
                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.DirectionFlash("Left");
                        }

                    }
                }
                if (centrelightbulb.GetComponent<LightbulbScript>().canFlash == true)
                {
                    if (currentFlashDirection == "Up")
                    {
                        centrelightbulb.SetTrigger("Flash");
                        centrelightbulb.GetComponent<LightbulbScript>().canFlash = false;
                        middleAnimator.SetTrigger("MiddleBackswing");
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TrainingBot/PreparePunch");
                        flashDirectionIndex += 1;
                        if (ArrowKeysScript.arrowKeys != null)
                        {
                            ArrowKeysScript.arrowKeys.DirectionFlash("Up");
                        }
                    }
                }



            }

            //dealing damage to player
            if (songPositionInBeats >= upperTimeToDodge)
            {
                if (dodged == false)
                {
                    Debug.Log("Deal damage");
                    dodged = true;
                    if (currentBeatDirectionToDodge == "Right")
                    {
                        PlayerBehaviour.player.animator.SetTrigger("TakeDamageLeft");
                        if (tutorialOn == false)
                        {
                            playerHealthSystem.TakeDamage(2);
                        }
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/TakeDamage");
                    }
                    if (currentBeatDirectionToDodge == "Left")
                    {
                        PlayerBehaviour.player.animator.SetTrigger("TakeDamageRight");
                        if (tutorialOn == false)
                        {
                            playerHealthSystem.TakeDamage(2);
                        }
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/TakeDamage");
                    }
                    if (currentBeatDirectionToDodge == "Up")
                    {
                        PlayerBehaviour.player.animator.SetTrigger("TakeDamageCentre");
                        if (tutorialOn == false)
                        {
                            playerHealthSystem.TakeDamage(2);
                        }
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/TakeDamage");
                    }
                }
            }

            if (songPositionInBeats >= nextHeadBob)
            {
                headBobIndex += 1;
                nextHeadBob = beatsWhereHeadBobs[headBobIndex];

                if (middleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral") || middleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blue"))
                {
                    if (playerAttacking == false)
                    {
                        //red
                        middleAnimator.SetTrigger("HeadBobRed");
                        if (leftAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral") || leftAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blue"))
                        {
                            leftAnimator.SetTrigger("HeadBobRed");

                        }
                        if (rightAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral") || rightAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blue"))
                        {
                            rightAnimator.SetTrigger("HeadBobRed");

                        }


                    }
                    else
                    {
                        //blue
                        middleAnimator.SetTrigger("HeadBobBlue");
                        if (leftAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral") || leftAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blue"))
                        {
                            leftAnimator.SetTrigger("HeadBobBlue");

                        }
                        if (rightAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral") || rightAnimator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blue"))
                        {
                            rightAnimator.SetTrigger("HeadBobBlue");

                        }

                    }
                }


            }
        }
    }

    public void PlayerPunch(string direction)
    {
        if (ableToHit == true)
        {
            playerBeatInput = songPositionInBeats;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Punch");
            if (playerBeatInput >= currentBeatToHit - 0.1f && playerBeatInput <= currentBeatToHit + 0.3f)
            {
                if(simplifiedOn == false)
                {
                    if (direction == currentBeatDirectionToHit)
                    {
                        Debug.Log("hit");
                        ableToHit = false;
                        opponentHealthSystem.TakeDamage(1);
                        if (tutorialTracker != null)
                        {
                            tutorialTracker.numberOfSuccesses += 1;

                        }
                        if (currentBeatDirectionToHit == "Left")
                        {
                            leftParticles.Play();
                            leftAnimator.SetTrigger("LeftBlock");
                            middleAnimator.SetTrigger("LeftBlock");
                        }
                        if (currentBeatDirectionToHit == "Up")
                        {
                            middleParticles.Play();
                            middleAnimator.SetTrigger("MiddleBlock");
                        }
                        if (currentBeatDirectionToHit == "Right")
                        {
                            rightParticles.Play();
                            rightAnimator.SetTrigger("RightBlock");
                            middleAnimator.SetTrigger("RightBlock");
                        }
                    }
                }
                else
                {
                    Debug.Log("hit");
                    ableToHit = false;
                    opponentHealthSystem.TakeDamage(1);
                    if (tutorialTracker != null)
                    {
                        tutorialTracker.numberOfSuccesses += 1;

                    }
                    if (currentBeatDirectionToHit == "Left")
                    {
                        leftParticles.Play();
                    }
                    if (currentBeatDirectionToHit == "Up")
                    {
                        middleParticles.Play();
                    }
                    if (currentBeatDirectionToHit == "Right")
                    {
                        rightParticles.Play();
                    }
                }
                
                
            }
        }
    }

    public void PlayerDodge(string direction)
    {
        
        if (ableToHit == true)
        {
            playerBeatInput = songPositionInBeats;
            
            if (playerBeatInput >= currentBeatToDodge - 0.1f && playerBeatInput <= currentBeatToDodge + 0.3f)
            {
                if(simplifiedOn == false)
                {
                    if (direction == currentBeatDirectionToDodge)
                    {
                        Debug.Log("dodge");
                        playerParticles.Play();
                        ableToHit = false;
                        dodged = true;
                        if (tutorialTracker != null)
                        {
                            tutorialTracker.numberOfSuccesses += 1;

                        }

                    }
                }
                else
                {
                    Debug.Log("dodge");
                    playerParticles.Play();
                    ableToHit = false;
                    dodged = true;
                    if (tutorialTracker != null)
                    {
                        tutorialTracker.numberOfSuccesses += 1;

                    }
                }
                

            }
        }
    }
}
