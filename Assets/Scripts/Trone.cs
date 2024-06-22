using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trone : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            RuneDialogue.showRune = true;
        }
    }
}
