using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del personaje que seguirá la cámara
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Distancia entre la cámara y el personaje

    void FixedUpdate()
    {
        // Calcula la posición a la que la cámara se moverá
        Vector3 desiredPosition = target.position + offset;
        // No cambia la posición en Z para mantener la cámara en 2D
        desiredPosition.z = transform.position.z;
        // Interpola suavemente entre la posición actual de la cámara y la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Asigna la posición suavizada a la posición de la cámara
        transform.position = smoothedPosition;
    }
}
