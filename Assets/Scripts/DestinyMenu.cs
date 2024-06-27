using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinyMenu : MonoBehaviour
{
    [SerializeField] private GameObject destinyMenu;

    public void Sit()
    {
        SceneManager.LoadScene(5);
    }

    public void NotSit()
    {
        Level3Manager.badEnding = true;
        destinyMenu.SetActive(false);
    }
}
