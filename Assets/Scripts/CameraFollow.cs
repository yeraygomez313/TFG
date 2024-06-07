using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del personaje que seguir� la c�mara
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Distancia entre la c�mara y el personaje

    private float posX, posY, posX2, posY2;
    [SerializeField] private float rightLimit, leftLimit, topLimit, botLimit;

    void FixedUpdate()
    {

        // Calcula la posici�n a la que la c�mara se mover�
        //Vector3 desiredPosition = target.position + offset;
        // No cambia la posici�n en Z para mantener la c�mara en 2D
        //desiredPosition.z = transform.position.z;
        // Interpola suavemente entre la posici�n actual de la c�mara y la posici�n deseada
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Asigna la posici�n suavizada a la posici�n de la c�mara
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
        // Asigna la posici�n suavizada a la posici�n de la c�mara
        transform.position = smoothedPosition;
    }
}
