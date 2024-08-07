using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Camera cameraPlayer;

    private SpriteRenderer enemySprite;

    [SerializeField] private Light2D lightEnemy;
    [SerializeField] private Light2D globalLight;

    [SerializeField] private AudioClip waterDrop1;
    [SerializeField] private AudioClip waterDrop2;
    [SerializeField] private AudioClip waterDrop3;
    [SerializeField] private AudioClip doorClosingSound;
    [SerializeField] private AudioClip backgroundDoorClosing;
    [SerializeField] private AudioClip airHiss;
    [SerializeField] private AudioClip lightBlink;
    [SerializeField] private AudioClip monsterGrowl;
    [SerializeField] private AudioClip monsterSteps;
    [SerializeField] private AudioClip monsterJumpscare;
    [SerializeField] private AudioClip monsterRunning;
    [SerializeField] private AudioClip monsterRunningViolin;
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
    private bool zoominCamera = false;
    private bool secondMove = false;
    
    public static bool turnOffMessage = false;
    public static bool flashlightEnergyDied = false;

    private float smoothSpeed = 0.01f;
    public static int batteries = 4;

    private Vector3 doorTargetPosition;
    private Vector3 enemyTargetPosition;
    private Vector3 newEnemyPosition;

    public bool playerLight = false;


    //PRUEBA DE MEN� DESPLEGABLE
    //public GameObject menuPanel; // Panel del men�
    //private bool isPaused = false;
    //PRUEBA DE MEN� DESPLEGABLE


    // Start is called before the first frame update
    void Start()
    {
        doorTargetPosition = new Vector3(door.transform.position.x, 7.36f, door.transform.position.z);
        enemyTargetPosition = new Vector3(55.15f, enemy.transform.position.y, enemy.transform.position.z);

        batteryPanel = batteryPanelInspector;
        batteryText = batteryTextInspector;

        enemySprite = enemy.GetComponent<SpriteRenderer>();
        //PRUEBA DE MEN� DESPLEGABLE
        //menuPanel.SetActive(false);
        //PRUEBA DE MEN� DESPLEGABLE
    }

    // Update is called once per frame
    void Update()
    {
        //PRUEBA DE MEN� DESPLEGABLE
        //if (isPaused && Input.GetMouseButtonDown(0))
        //{
        //    ResumeGame();
        //}
        //PRUEBA DE MEN� DESPLEGABLE

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
            audioEffects3.clip = monsterGrowl;
            audioEffects3.Play();
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

        if (player.transform.position.x > 133 && playOnce)
        {
            playOnce = false;
            StartCoroutine(EnemyRunning());
        }

        if (zoominCamera)
        {
            cameraPlayer.orthographicSize = cameraPlayer.orthographicSize = Mathf.Lerp(cameraPlayer.orthographicSize, cameraPlayer.orthographicSize + 2f, Time.deltaTime / 2.0f);
            if (cameraPlayer.orthographicSize >= 9.0f)
                zoominCamera = false;
        }

        if (secondMove)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, newEnemyPosition, 0.005f);
            if (Vector3.Distance(enemy.transform.position, newEnemyPosition) < 0.3f)
            {
                enemy.transform.position = newEnemyPosition;
                secondMove = false;
            }
        }
            
        if (player.transform.position.x > 176f && player.transform.position.y < 19f)
            SceneManager.LoadScene(4);

    }

    //PRUEBA DE MEN� DESPLEGABLE
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

    //// Llama a este m�todo cuando cojas un objeto
    //public void OnObjectPicked()
    //{
    //    ShowMenu();
    //}
    //PRUEBA DE MEN� DESPLEGABLE

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

    private void ControlPlayerMovement(bool continueMoving)
    {
        if (continueMoving)
        {
            playerMovement.movementEnabled = true;
        }
        else
        {
            playerMovement.rb.velocity = new Vector2(0f, 0f);
            playerMovement.movementEnabled = false;
            playerMovement.animator.SetFloat("Horizontal", 0);
        }
    }

    private IEnumerator FlashlightEnergyDied()
    {
        enemySprite.sortingLayerName = "Frontground";
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
        yield return new WaitForSeconds(2.0f);
        audioEffects3.clip = monsterSteps;
        audioEffects3.Play();
        yield return new WaitForSeconds(8.0f);
        //Sonido de miedo que acompa�e los movimientos del enemigo
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(player.transform.position.x + 5.0f, player.transform.position.y, enemy.transform.position.z);
        yield return new WaitForSeconds(.1f);
        enemy.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5.0f, enemy.transform.position.z);
        yield return new WaitForSeconds(.1f);
        enemy.transform.position = new Vector3(player.transform.position.x - 5.0f, player.transform.position.y, enemy.transform.position.z);
        yield return new WaitForSeconds(.1f);
        enemy.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, enemy.transform.position.z);
        enemy.transform.localScale *= 5f;
        audioEffects3.Stop();
        audioEffects3.clip = monsterJumpscare;
        audioEffects3.Play();
        yield return new WaitForSeconds(.1f);
        enemy.SetActive(false);
        yield return new WaitForSeconds(.1f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(.1f);
        enemy.SetActive(false);
        yield return new WaitForSeconds(.1f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator EnemyRunning()
    {
        //ControlPlayerMovement(false);
        zoominCamera = true;
        //cameraPlayer.orthographicSize = cameraPlayer.orthographicSize = Mathf.Lerp(cameraPlayer.orthographicSize, cameraPlayer.orthographicSize + 2f, Time.deltaTime / 2.0f);
        yield return new WaitForSeconds(4.0f);
        cameraPlayer.orthographicSize = 9.0f;
        //ControlPlayerMovement(true);
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(121.13f, 36.33f, 0f);
        enemySprite.flipX = false;

        yield return new WaitForSeconds(4.0f);

        newEnemyPosition = new Vector3(194.64f, enemy.transform.position.y, enemy.transform.position.z);


        secondMove = true;
        yield return new WaitForSeconds(.2f);
        audioEffects2.clip = monsterRunning;
        audioEffects3.clip = monsterRunningViolin;
        audioEffects2.Play();
        audioEffects3.Play();
        globalLight.intensity = 1f;
        yield return new WaitForSeconds(.05f);
        globalLight.intensity = .0f;
        yield return new WaitForSeconds(.05f);
        globalLight.intensity = 1f;
        yield return new WaitForSeconds(.05f);
        globalLight.intensity = .1f;

        //while (Vector3.Distance(enemy.transform.position, newEnemyPosition) > 0.3f)
        //    enemy.transform.position = Vector3.Lerp(enemy.transform.position, newEnemyPosition, smoothSpeed);
        yield return new WaitForSeconds(4.0f);
        //enemy.transform.position = newEnemyPosition;
    }
}
