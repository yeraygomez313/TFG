using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCameraFollow : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    private float smoothSpeed = .01f;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, cameraFollow.transform.position.y, 0);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraFollow.transform.position.y + .75f, 0), smoothSpeed);
        // Asigna la posición suavizada a la posición de la cámara
        transform.position = smoothedPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, cameraFollow.transform.position.y, 0);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraFollow.transform.position.y + .75f, 0), smoothSpeed);
        // Asigna la posición suavizada a la posición de la cámara
        transform.position = smoothedPosition;
    }
}
