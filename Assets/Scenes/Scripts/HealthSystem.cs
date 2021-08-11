using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject healthbarPrefab;
    HealthBar myHealthBar;
    public Canvas canvas;
    public bool takenDamage;
    public GameObject gameOverSplash;
    float timeSinceTakenDamage;
    public float timeUntilCanTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameObject healthBarObject = Instantiate(healthbarPrefab, canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBar>();
        takenDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        timeSinceTakenDamage += Time.deltaTime;
    }

    public void TakeDamage(float damageAmount)
    {
        if(timeSinceTakenDamage >= timeUntilCanTakeDamage)
        {
            if (currentHealth > 0)
            {
                currentHealth -= damageAmount;
                takenDamage = true;
                timeSinceTakenDamage = 0;

                if (currentHealth <= 0)
                {
                    //Player wins
                    if (gameObject.GetComponent<PlayerBehaviour>() != null)
                    {
                        Debug.Log("Player Wins");
                    }

                    //Opponent wins
                    if (gameObject.GetComponent<TrainingBotScript>() != null)
                    {
                        Debug.Log("Opponent Wins");
                    }

                }
            }
        }
        

        if (currentHealth <= 0)
        {
            if (gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                Instantiate(gameOverSplash);
                Conductor.instance.GetComponent<AudioSource>().Pause();
                Conductor.instance.enabled = false;
                
                
            }
        }
    }

    

    private void OnDestroy()
    {

        if (gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        }
        else
        {
            PlayerPrefs.SetFloat("EnemyHealth", currentHealth);
        }
        Debug.Log(PlayerPrefs.GetFloat("PlayerHealth"));
        Debug.Log(PlayerPrefs.GetFloat("EnemyHealth"));
    }
}
