using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door;

    private bool firstEvent = true;
    private bool doorClosing = false;
    private float smoothSpeed = 0.01f;

    private Vector3 doorTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        doorTargetPosition = new Vector3(door.transform.position.x, 7.36f, door.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < 29f && firstEvent)
        {
            Debug.Log("activo el evento");
            firstEvent = false;
            Invoke("StartClosingDoor", 0.5f);
        }

        if (doorClosing)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, doorTargetPosition, smoothSpeed);
            if (Vector3.Distance(door.transform.position, doorTargetPosition) < 0.01f)
            {
                door.transform.position = doorTargetPosition;
                doorClosing = false;
                playerMovement.movementEnabled = true;
            }
        }
    }

    void StartClosingDoor()
    {
        Debug.Log("ejecuto el evento");
        doorClosing = true;
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
    }
}
