using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trone : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool playOnce = true;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playOnce)
        {
            playOnce = false;
            panel.SetActive(true);
            RuneDialogue.showRune = true;
        }
    }
}
