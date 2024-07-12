using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI; // Arrastra tu menú UI aquí en el editor
    private bool isPaused = false;

    void Update()
    {
        // Comprueba si se ha pulsado la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        menuUI.SetActive(false); // Oculta el menú
        Time.timeScale = 1f; // Reanuda el tiempo
        isPaused = false;
    }

    void Pause()
    {
        menuUI.SetActive(true); // Muestra el menú
        Time.timeScale = 0f; // Pausa el tiempo
        isPaused = true;
    }

    public void Lvl1()
    {
        // Reanuda el tiempo antes de cargar la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Lvl2()
    {
        // Reanuda el tiempo antes de cargar la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void Lvl3()
    {
        // Reanuda el tiempo antes de cargar la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    public void Menu()
    {
        // Reanuda el tiempo antes de cargar la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
