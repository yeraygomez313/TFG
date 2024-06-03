using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLight : MonoBehaviour
{

    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject player;
    [SerializeField] private Light2D playerFlashlight;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerFlashlight.intensity = 1;
            flashlight.SetActive(false);
        }
    }
}
