using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public int currentScene;
    public Dialogue dialogue;
    public bool finalScene;
    public GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogue = GetComponent<DialogueTrigger>().dialogue;
        StartDialogue(dialogue);
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
   
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.01f);
            yield return null;
        }
    }

    void EndDialogue()
    {
        if(finalScene == false)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
        
    }
}
