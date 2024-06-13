using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ManageEnergyFlashLight : MonoBehaviour
{

    [SerializeField] private Light2D playerFlashlight;

    [SerializeField] private float decayRate = 10f; // Velocidad a la que disminuirán los radios

    [SerializeField] private Image rechargeImage;

    private bool playOnce = true;

    public static bool startManagement = false;
    public static bool increaseRadius = false;

    // Update is called once per frame
    void Update()
    {
        if (startManagement)
        {
            if (playOnce)
            {
                playOnce = false;
                rechargeImage.gameObject.SetActive(true);
            }
            
            // Reducir gradualmente los radios inner y outer de la linterna
            playerFlashlight.pointLightInnerRadius = Mathf.Max(0, playerFlashlight.pointLightInnerRadius - decayRate * Time.deltaTime);
            playerFlashlight.pointLightOuterRadius = Mathf.Max(0, playerFlashlight.pointLightOuterRadius - decayRate * Time.deltaTime);

            // Detener la gestión cuando ambos radios sean 0
            if (playerFlashlight.pointLightInnerRadius <= 0 && playerFlashlight.pointLightOuterRadius <= 0)
            {
                startManagement = false;
                Level2Manager.flashlightEnergyDied = true;
            }

            if (increaseRadius || Input.GetKeyDown(KeyCode.R))
            {
                increaseRadius = false;
                playerFlashlight.pointLightInnerRadius = 3f;
                playerFlashlight.pointLightOuterRadius = 4f;
            }

        }
    }
}
