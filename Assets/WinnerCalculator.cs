using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerCalculator : MonoBehaviour
{
    public Text enemyHealth;
    public Text playerHealth;
    public HealthBar enemyHealthBar;
    public HealthBar playerHealthBar;
    public Text winnerText;
    public string whenEnemyWins;
    public string whenPlayerWins;

    public GameObject enemyBlock;
    public GameObject playerBlock;
    public GameObject winnerBlock;
    // Start is called before the first frame update
    void Start()
    {
        enemyBlock.SetActive(false);
        playerBlock.SetActive(false);
        winnerBlock.SetActive(false);
        
        enemyHealthBar.ShowHealthFraction(PlayerPrefs.GetFloat("EnemyHealth")/ 26);
        playerHealthBar.ShowHealthFraction(PlayerPrefs.GetFloat("PlayerHealth")/ 26);
        if(PlayerPrefs.GetFloat("EnemyHealth") > PlayerPrefs.GetFloat("PlayerHealth"))
        {
            winnerText.text = whenEnemyWins;
            winnerText.color = Color.grey;
        }
        if (PlayerPrefs.GetFloat("EnemyHealth") < PlayerPrefs.GetFloat("PlayerHealth"))
        {
            winnerText.text = whenPlayerWins;
            winnerText.color = Color.yellow;
        }
        if (PlayerPrefs.GetFloat("EnemyHealth") == PlayerPrefs.GetFloat("PlayerHealth"))
        {
            winnerText.text = "Tie!";
        }
        StartCoroutine(WinnerCalculation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WinnerCalculation()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        enemyBlock.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        playerBlock.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        winnerBlock.SetActive(true);
        yield return null;
    }
}
