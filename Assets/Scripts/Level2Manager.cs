using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject flashlight;

    [SerializeField] private Light2D lightEnemy;

    [SerializeField] private AudioClip waterDrop1;
    [SerializeField] private AudioClip waterDrop2;
    [SerializeField] private AudioClip waterDrop3;
    [SerializeField] private AudioClip doorClosingSound;
    [SerializeField] private AudioClip backgroundDoorClosing;
    [SerializeField] private AudioClip airHiss;
    [SerializeField] private AudioClip lightBlink;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource audioEffects2;
    [SerializeField] private AudioSource audioEffects3;
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioSource audioMusic2;
    [SerializeField] private AudioSource audioMusic3;
    [SerializeField] private AudioSource audioMusic4;

    public GameObject batteryPanelInspector;
    public TMP_Text batteryTextInspector;
    public static GameObject batteryPanel;
    public static TMP_Text batteryText;

    private bool firstEvent = true;
    private bool secondEvent = true;
    private bool doorClosing = false;
    private bool enemyMoving = false;
    private bool lightBlinks = false;
    private bool playOnce = true; //Play once the air hissing sound
    
    public static bool turnOffMessage = false;
    public static bool flashlightEnergyDied = false;

    private float smoothSpeed = 0.01f;
    public static int batteries = 4;

    private Vector3 doorTargetPosition;
    private Vector3 enemyTargetPosition;

    public bool playerLight = false;


    //PRUEBA DE MENÚ DESPLEGABLE
    //public GameObject menuPanel; // Panel del menú
    //private bool isPaused = false;
    //PRUEBA DE MENÚ DESPLEGABLE


    // Start is called before the first frame update
    void Start()
    {
        doorTargetPosition = new Vector3(door.transform.position.x, 7.36f, door.transform.position.z);
        enemyTargetPosition = new Vector3(55.15f, enemy.transform.position.y, enemy.transform.position.z);

        batteryPanel = batteryPanelInspector;
        batteryText = batteryTextInspector;
        //PRUEBA DE MENÚ DESPLEGABLE
        //menuPanel.SetActive(false);
        //PRUEBA DE MENÚ DESPLEGABLE
    }

    // Update is called once per frame
    void Update()
    {
        //PRUEBA DE MENÚ DESPLEGABLE
        //if (isPaused && Input.GetMouseButtonDown(0))
        //{
        //    ResumeGame();
        //}
        //PRUEBA DE MENÚ DESPLEGABLE

        //Primer evento de cerrar puerta
        if (player.transform.position.x < 29f && firstEvent)
        {
            print("primer evento");
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
                audioEffects3.clip = airHiss;
                audioEffects3.Play();   //HACER QUE ESTO SUENE ANTEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEES
            }
        }

        //Segundo evento del enemigo apareciendo debajo de la luz
        if (player.transform.position.x > 49f && secondEvent)
        {
            secondEvent = false;
            Invoke("StartMovingEnemy", .5f);
        }

        if (enemyMoving)
        {

            enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemyTargetPosition, smoothSpeed);
            if (Vector3.Distance(enemy.transform.position, enemyTargetPosition) < 0.3f)
            {
                enemy.transform.position = enemyTargetPosition;
                enemyMoving = false;
                lightBlinks = true;
            }
        }

        if (lightBlinks)
        {
            lightBlinks = false;
            lightEnemy.intensity = 0.65f;
            audioEffects.clip = lightBlink;
            audioEffects.Play();
            Invoke("EnableEnemyLight", .25f);
            Invoke("DisableEnemyLight", .5f);
            Invoke("EnableEnemyLight", .75f);
            Invoke("DisableEnemyLight", 1f);
            Invoke("EnableEnemyLight", 1.25f);
            Invoke("DisableEnemyLight", 1.5f);
            Invoke("EnemyDisappear", 1.5f);
        }

        if (turnOffMessage)
        {
            turnOffMessage = false;
            Invoke("DisableBatteryMessage", 4.0f);
        }
        Debug.Log("este: " + flashlightEnergyDied);
        if (flashlightEnergyDied)
        {
            flashlightEnergyDied = false;
            StartCoroutine(FlashlightEnergyDied());
        }
    }

    //PRUEBA DE MENÚ DESPLEGABLE
    //public void ShowMenu()
    //{
    //    isPaused = true;
    //    menuPanel.SetActive(true);
    //    Time.timeScale = 0f; // Pausa el juego
    //}

    //public void ResumeGame()
    //{
    //    isPaused = false;
    //    menuPanel.SetActive(false);
    //    Time.timeScale = 1f; // Reanuda el juego
    //}

    //// Llama a este método cuando cojas un objeto
    //public void OnObjectPicked()
    //{
    //    ShowMenu();
    //}
    //PRUEBA DE MENÚ DESPLEGABLE

    void StartClosingDoor()
    {
        Debug.Log("ejecuto el evento");
        doorClosing = true;
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
        audioEffects.clip = doorClosingSound;
        audioEffects.Play();
        Invoke("StopDoorSound", 3.5f);
        audioEffects2.clip = backgroundDoorClosing;
        audioEffects2.Play();
    }

    void StopDoorSound()
    {
        audioEffects.Stop();
    }

    void StartMovingEnemy()
    {
        enemyMoving = true;
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
    }

    void EnableEnemyLight()
    {
        lightEnemy.intensity = 0.65f;
    }

    void DisableEnemyLight()
    {
        lightEnemy.intensity = 0f;
    }

    void EnemyDisappear()
    {
        enemy.SetActive(false);
        flashlight.SetActive(true);
        playerMovement.movementEnabled = true;
    }

    public static void ShowBatteryMessage()
    {
        if (batteries > 1)
            batteryText.text = "There are " + batteries + " batteries left";
        else if (batteries == 1)
            batteryText.text = "There is " + batteries + " battery left";
        else
            batteryText.text = "You got all the batteries";

        batteryPanel.SetActive(true);
        turnOffMessage = true;
    }

    void DisableBatteryMessage()
    {
        batteryPanel.SetActive(false);
    }

    private IEnumerator FlashlightEnergyDied()
    {
        yield return new WaitForSeconds(2.0f);
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
        //Sonido de miedo que acompañe los movimientos del enemigo
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(player.transform.position.x + 5.0f, player.transform.position.y, enemy.transform.position.z);
        yield return new WaitForSeconds(.5f);
        enemy.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5.0f, enemy.transform.position.z);
        yield return new WaitForSeconds(.5f);
        enemy.transform.position = new Vector3(player.transform.position.x - 5.0f, player.transform.position.y, enemy.transform.position.z);
    }
}
