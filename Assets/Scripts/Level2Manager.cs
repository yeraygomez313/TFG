using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door;

    private bool firstEvent = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < 27.0f && firstEvent)
        {
            Debug.Log("activo el evento");
            firstEvent = false;
            Invoke("CloseDoor", 0.5f);
        }
    }

    void CloseDoor()
    {
        Debug.Log("ejecuto el evento");
        //door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(door.transform.position.x, 20f, door.transform.position.z), 1.0f);

        //door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(door.transform.position.x, 20.0f + .75f, door.transform.position.z), .01f);

        Vector3 targetPosition = new Vector3(door.transform.position.x, -100.0f, door.transform.position.z);
        door.transform.position = Vector3.Lerp(door.transform.position, targetPosition, .01f);

    }
}
