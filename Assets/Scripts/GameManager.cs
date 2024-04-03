using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioSource audioSource;

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 40.0f && once)
        {
            playerMovement.movementEnabled = false;
            once = false;
            audioSource.PlayOneShot(crashSound);
            Invoke("mirar", 1);
            Invoke("mirar2", 2);
            Invoke("texto", 2);
            Invoke("moverse", 1);
        }
    }

    void mirar()
    {
        playerMovement.playerSprite.flipX = true;
    }

    void mirar2()
    {
        playerMovement.playerSprite.flipX = false;
    }

    void texto()
    {
        // Crear una instancia del objeto en el que deseas instanciar el script
        GameObject nuevoObjeto = GameObject.Find("Texto1");

        // Agregar el script SelfDialogue al nuevo objeto
        SelfDialogue selfDialogue = nuevoObjeto.GetComponent<SelfDialogue>();

        // Activar el diálogo
        selfDialogue.StartDialogue();
    }

    void moverse()
    {
        playerMovement.movementEnabled = true;
    }
}
