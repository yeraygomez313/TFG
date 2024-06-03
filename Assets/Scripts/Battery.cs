using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private GameObject battery;

    [SerializeField] private float amplitude = 0.5f; // Amplitud del movimiento (qu� tan alto/sube bajo)
    [SerializeField] private float speed = 1.0f; // Velocidad del movimiento

    private Vector3 startPosition;

    void Start()
    {
        // Guarda la posici�n inicial del GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcula la posici�n vertical utilizando una funci�n sinusoidal
        float yPos = startPosition.y + amplitude * Mathf.Sin(speed * Time.time);

        // Actualiza la posici�n del GameObject
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
