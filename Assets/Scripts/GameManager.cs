using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Camera cameraPlayer;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private EnemyChasing enemyChasing;
    [SerializeField] private GameObject jumpscareImage;
    [SerializeField] private GameObject parallax;

    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip creepyViolins;
    [SerializeField] private AudioClip chasingViolins;
    [SerializeField] private AudioClip jumpscareSound;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource audioMusic;
    

    private bool firstEvent = true;
    private bool secondEvent = true;
    private bool playOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parallax.transform.position = new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y+3.5f, 0);

        // Primer evento
        if (player.transform.position.x > 40.0f && firstEvent)
        {
            firstEvent = false;
            eventHandler(crashSound);
            Invoke("mirar", 0.5f);
            Invoke("mirar2", 2);
            Invoke("texto", 2);
            Invoke("moverse", 4.5f);
        }
        // Segundo evento
        if (player.transform.position.x > 99.0f && secondEvent)
        {
            enemy.SetActive(true);
            playerMovement.audioSource.Stop();
            secondEvent = false;
            eventHandler(creepyViolins);
            Invoke("focusEnemy", 0.5f);
            Invoke("startChasing", 4f);
            Invoke("focusPlayer", 4.5f);
            Invoke("moverse", 4.6f);
        }
        if (enemyChasing.jumpscare && playOnce)
        {
            jumpscareImage.SetActive(true);
            audioEffects.PlayOneShot(jumpscareSound);
            playOnce = false;
        }
    }

    // Esta función prepara el evento que ha sido detectado en el Update() y se encarga de gestionar los sonidos y parar el movimiento del jugador
    void eventHandler(AudioClip playAudio)
    {
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
        audioEffects.PlayOneShot(playAudio);
    }

    void startChasing()
    {
        audioMusic.loop = true;
        audioMusic.clip = chasingViolins;
        audioMusic.Play();
        enemyChasing.canChase = true;
    }

    void focusPlayer()
    {
        cameraFollow.target = player.transform;
    }

    void focusEnemy()
    {
        cameraFollow.target = enemy.transform;
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
