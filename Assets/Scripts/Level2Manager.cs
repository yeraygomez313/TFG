using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Light2D lightEnemy;

    private bool firstEvent = true;
    private bool secondEvent = true;
    private bool doorClosing = false;
    private bool enemyMoving = false;
    private bool lightBlinks = false;

    private float smoothSpeed = 0.01f;

    private Vector3 doorTargetPosition;
    private Vector3 enemyTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        doorTargetPosition = new Vector3(door.transform.position.x, 7.36f, door.transform.position.z);
        enemyTargetPosition = new Vector3(55.15f, enemy.transform.position.y, enemy.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        //Primer evento de cerrar puerta
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

        //Segundo evento del enemigo apareciendo debajo de la luz
        if (player.transform.position.x > 49f && secondEvent)
        {
            secondEvent = false;
            Invoke("StartMovingEnemy", 0.5f);
        }

        if (enemyMoving)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemyTargetPosition, smoothSpeed);
            if (Vector3.Distance(enemy.transform.position, enemyTargetPosition) < 0.3f)
            {
                enemy.transform.position = enemyTargetPosition;
                enemyMoving = false;
                playerMovement.movementEnabled = true;
                lightBlinks = true;
            }
        }

        if (lightBlinks)
        {
            lightBlinks = false;
            lightEnemy.intensity = 0.65f;
            Invoke("enableEnemyLight", .25f);
            Invoke("disableEnemyLight", .5f);
            Invoke("enableEnemyLight", .75f);
            Invoke("disableEnemyLight", 1f);
            Invoke("enableEnemyLight", 1.25f);
            Invoke("disableEnemyLight", 1.5f);
            Invoke("enemyDisappear", 1.5f);
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

    void StartMovingEnemy()
    {
        enemyMoving = true;
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
    }

    void enableEnemyLight()
    {
        lightEnemy.intensity = 0.65f;
    }

    void disableEnemyLight()
    {
        lightEnemy.intensity = 0f;
    }

    void enemyDisappear()
    {
        enemy.SetActive(false);
    }
}
