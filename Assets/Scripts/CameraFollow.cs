using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del personaje que seguir� la c�mara
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Distancia entre la c�mara y el personaje

    void FixedUpdate()
    {
        // Calcula la posici�n a la que la c�mara se mover�
        Vector3 desiredPosition = target.position + offset;
        // No cambia la posici�n en Z para mantener la c�mara en 2D
        desiredPosition.z = transform.position.z;
        // Interpola suavemente entre la posici�n actual de la c�mara y la posici�n deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Asigna la posici�n suavizada a la posici�n de la c�mara
        transform.position = smoothedPosition;
    }
}
