using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlinks : MonoBehaviour
{
    public Light2D light2D; // Referencia a la luz Light2D
    public float minBlinkInterval = 0.5f; // Tiempo mínimo entre parpadeos
    public float maxBlinkInterval = 2f; // Tiempo máximo entre parpadeos

    private void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }

        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight()
    {
        while (true)
        {
            float waitTime = Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(waitTime);
            light2D.enabled = !light2D.enabled;
        }
    }
}
