using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinyMenu : MonoBehaviour
{
    [SerializeField] private GameObject destinyMenu;

    [SerializeField] private AudioSource music1;
    [SerializeField] private AudioSource music2;
    [SerializeField] private PlayerMovement playerMovement;

    public void Sit()
    {
        SceneManager.LoadScene(5);
    }

    public void NotSit()
    {
        Level3Manager.move = false;
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = true;
        playerMovement.animator.SetFloat("Horizontal", 0);
        music1.Stop();
        music2.Stop();
        Level3Manager.badEnding = true;
        destinyMenu.SetActive(false);
    }
}
