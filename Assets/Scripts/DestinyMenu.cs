using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinyMenu : MonoBehaviour
{
    [SerializeField] private GameObject destinyMenu;

    [SerializeField] private AudioSource music1;
    [SerializeField] private AudioSource music2;

    public void Sit()
    {
        SceneManager.LoadScene(5);
    }

    public void NotSit()
    {
        music1.Stop();
        music2.Stop();
        Level3Manager.badEnding = true;
        destinyMenu.SetActive(false);
    }
}
