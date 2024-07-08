using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Camera cameraPlayer;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AudioClip badEndingSong;
    [SerializeField] private AudioSource music;

    private bool playOnce = true;
    private bool playOnce2 = true;
    private bool zoominCamera = false;
    private bool zoominCamera2 = false;
    public static bool move = false;

    public static bool badEnding = false;


    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            playerMovement.rb.velocity = new Vector2(0f, 0f);
            playerMovement.movementEnabled = false;
            playerMovement.animator.SetFloat("Horizontal", 0);
        }

        if (badEnding)
        {
            music.clip = badEndingSong;
            music.Play();
            badEnding = false;
            foreach (GameObject enemie in enemies)
                enemie.SetActive(true);
        }

        if (player.transform.position.x > 33.0f && player.transform.position.y < -46.0f && playOnce)
        {
            zoominCamera = true;
            playOnce = false;
        }

        if (player.transform.position.x > 130.0f && player.transform.position.y < -46.0f && playOnce2)
        {
            zoominCamera2 = true;
            playOnce2 = false;
        }

        if (zoominCamera)
        {
            cameraPlayer.orthographicSize = cameraPlayer.orthographicSize = Mathf.Lerp(cameraPlayer.orthographicSize, cameraPlayer.orthographicSize - 2f, Time.deltaTime / 2.0f);
            if (cameraPlayer.orthographicSize <= 3.0f)
                zoominCamera = false;
        }

        if (zoominCamera2)
        {
            cameraPlayer.orthographicSize = cameraPlayer.orthographicSize = Mathf.Lerp(cameraPlayer.orthographicSize, cameraPlayer.orthographicSize + 2f, Time.deltaTime / 2.0f);
            if (cameraPlayer.orthographicSize >= 9.0f)
                zoominCamera2 = false;
        }
    }
}
