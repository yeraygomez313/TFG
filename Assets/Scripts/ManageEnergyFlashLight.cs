using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ManageEnergyFlashLight : MonoBehaviour
{

    [SerializeField] private Light2D playerFlashlight;

    [SerializeField] private float decayRate = 10f; // Velocidad a la que disminuirán los radios

    public static bool startManagement = false;
    public static bool increaseRadius = false;

    // Update is called once per frame
    void Update()
    {
        if (startManagement)
        {
            // Reducir gradualmente los radios inner y outer de la linterna
            playerFlashlight.pointLightInnerRadius = Mathf.Max(0, playerFlashlight.pointLightInnerRadius - decayRate * Time.deltaTime);
            playerFlashlight.pointLightOuterRadius = Mathf.Max(0, playerFlashlight.pointLightOuterRadius - decayRate * Time.deltaTime);

            // Detener la gestión cuando ambos radios sean 0
            if (playerFlashlight.pointLightInnerRadius <= 0 && playerFlashlight.pointLightOuterRadius <= 0)
            {
                startManagement = false;
            }

            if (increaseRadius)
            {
                playerFlashlight.pointLightInnerRadius = 3f;
                playerFlashlight.pointLightOuterRadius = 4f;
            }
        }
    }
}
