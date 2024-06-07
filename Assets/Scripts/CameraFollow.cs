using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del personaje que seguirá la cámara
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Distancia entre la cámara y el personaje

    private float posX, posY, posX2, posY2;
    [SerializeField] private float rightLimit, leftLimit, topLimit, botLimit;

    void FixedUpdate()
    {

        // Calcula la posición a la que la cámara se moverá
        //Vector3 desiredPosition = target.position + offset;
        // No cambia la posición en Z para mantener la cámara en 2D
        //desiredPosition.z = transform.position.z;
        // Interpola suavemente entre la posición actual de la cámara y la posición deseada
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Asigna la posición suavizada a la posición de la cámara
        //transform.position = smoothedPosition;

        
        posX = target.transform.position.x;
        posY = target.transform.position.y;


        if (target.transform.position.x > rightLimit && target.transform.position.x < leftLimit)
        {
            posX2 = posX;
        }

        if (target.transform.position.y > topLimit && target.transform.position.y < botLimit)
        {
            posY2 = posY;
        }
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(posX2, target.transform.position.y + 1.5f, -1), smoothSpeed);
        // Asigna la posición suavizada a la posición de la cámara
        transform.position = smoothedPosition;
    }
}
