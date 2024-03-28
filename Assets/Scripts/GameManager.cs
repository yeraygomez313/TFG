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
            once = false;
            audioSource.PlayOneShot(crashSound);
            Invoke("mirar", 1);
            Invoke("mirar2", 2);
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
}
