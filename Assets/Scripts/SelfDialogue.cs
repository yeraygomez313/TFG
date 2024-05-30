using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private AudioClip letterSound; // Sonido para cada letra
    [SerializeField] private AudioSource audioSource; // AudioSource para reproducir el sonido
    [SerializeField] private float positionGreaterThan = 40.0f;   // Posición en la que se mostrará el texto

    private float typingTime = 0.05f;

    private bool didDialogueStart;
    private int lineIndex;

    private void Start()
    {
        dialogueText.color = Color.magenta;
    }

    // Update is called once per frame
    void Update()
    {
        if (didDialogueStart)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (dialogueText.text == dialogueLines[lineIndex])
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

    // Llamar cuando se quiera comenzar un diálogo
    public void StartDialogue()
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
