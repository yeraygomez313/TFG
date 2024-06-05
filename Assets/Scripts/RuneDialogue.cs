using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneDialogue : MonoBehaviour
{

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private AudioClip letterSound; // Sonido para cada letra
    [SerializeField] private AudioSource audioSource; // AudioSource para reproducir el sonido

    private float typingTime = 0.05f;

    private bool firstTime = true;  //Poner a true cuando se quiera mostrar una runa
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    public static bool showRune = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (showRune)
        {
            if (firstTime || Input.GetButtonDown("Fire1"))
            {
                firstTime = false;
                if (!didDialogueStart)
                {
                    StartDialogue();
                }
                else if (dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                    
                }
            }
        }
        
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            showRune = false;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            if (letterSound != null)
            {
                audioSource.PlayOneShot(letterSound); // Reproducir sonido por cada letra
            }
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
}
