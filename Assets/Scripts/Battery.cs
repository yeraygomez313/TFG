using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private GameObject battery;

    [SerializeField] private float amplitude = 0.5f; // Amplitud del movimiento (qué tan alto/sube bajo)
    [SerializeField] private float speed = 1.0f; // Velocidad del movimiento

    private Vector3 startPosition;

    void Start()
    {
        // Guarda la posición inicial del GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcula la posición vertical utilizando una función sinusoidal
        float yPos = startPosition.y + amplitude * Mathf.Sin(speed * Time.time);

        // Actualiza la posición del GameObject
        transform.position = new Vector3(startPosition.x, yPos, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ManageEnergyFlashLight.increaseRadius = true;
            battery.SetActive(false);
        }
            
    }
}
