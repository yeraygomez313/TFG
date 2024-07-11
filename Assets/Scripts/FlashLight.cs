using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLight : MonoBehaviour
{

    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject player;
    [SerializeField] private Light2D playerFlashlight;
    [SerializeField] private GameObject wall;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wall.SetActive(false);
            playerFlashlight.intensity = 1;
            ManageEnergyFlashLight.startManagement = true;
            playerFlashlight.pointLightInnerRadius = 3f;
            playerFlashlight.pointLightOuterRadius = 4f;
            RuneDialogue.showRune = true;
            flashlight.SetActive(false);
        }
    }
}
