using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comic : MonoBehaviour
{
    [SerializeField] private AudioClip page;
    [SerializeField] private AudioSource audioEffects;

    public Image[] images;  // Array de imágenes
    private int currentImageIndex = 0;

    void Start()
    {
        audioEffects.clip = page;
        // Asegúrate de que solo la primera imagen está visible al inicio
        for (int i = 1; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Detecta si se hace clic con el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextImageOrChangeScene();
        }
    }

    void ShowNextImageOrChangeScene()
    {
        if (currentImageIndex < images.Length - 1)
        {
            audioEffects.Play();
            currentImageIndex++;
            images[currentImageIndex].gameObject.SetActive(true);
        }
        else
        {
            // Cambia de escena después de que todas las imágenes se hayan mostrado
            SceneManager.LoadScene(0);
        }
    }
}
