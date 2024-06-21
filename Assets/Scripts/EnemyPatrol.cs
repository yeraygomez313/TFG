using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip monsterJumpscare;
    [SerializeField] private AudioSource audioEffects;

    public float leftLimit; // L�mite izquierdo de la patrulla en el eje X
    public float rightLimit; // L�mite derecho de la patrulla en el eje X
    public float speed = 2f; // Velocidad de movimiento del enemigo

    private SpriteRenderer enemySprite;

    private bool movingRight = true; // Bandera para la direcci�n de movimiento
    private bool isUnderLight = false; // Bandera para indicar si est� bajo el efecto de una luz
    private bool moving = true;

    private void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (moving)
        {
            // Mover el enemigo hacia la derecha o izquierda
            if (movingRight)
            {
                enemySprite.flipX = false;
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                // Si el enemigo ha alcanzado el l�mite derecho, cambiar de direcci�n
                if (transform.position.x >= rightLimit)
                {
                    movingRight = false;
                }
            }
            else
            {
                enemySprite.flipX = true;
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                // Si el enemigo ha alcanzado el l�mite izquierdo, cambiar de direcci�n
                if (transform.position.x <= leftLimit)
                {
                    movingRight = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobar si el enemigo ha entrado en el �rea de la luz
        if (collision.CompareTag("LightArea"))
        {
            // Hacer que el enemigo se haga invisible
            enemySprite.enabled = false;
            // Actualizar la bandera de estar bajo luz
            isUnderLight = true;
        }
        // Comprobar si el enemigo ha colisionado con el jugador
        if (!isUnderLight && collision.gameObject.CompareTag("Player"))
        {
            moving = false;
            StartCoroutine(KillPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Comprobar si el enemigo ha salido del �rea de la luz
        if (collision.CompareTag("LightArea"))
        {
            // Hacer que el enemigo se haga visible de nuevo
            enemySprite.enabled = true;
            // Actualizar la bandera de estar bajo luz
            isUnderLight = false;
        }
    }

    private IEnumerator KillPlayer()
    {
        enemySprite.sortingLayerName = "Frontground";
        playerMovement.rb.velocity = new Vector2(0f, 0f);
        playerMovement.movementEnabled = false;
        playerMovement.animator.SetFloat("Horizontal", 0);
        //Sonido de miedo que acompa�e los movimientos del enemigo
        transform.position = new Vector3(player.transform.position.x + 2.0f, player.transform.position.y, transform.position.z);
        yield return new WaitForSeconds(.1f);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, transform.position.z);
        yield return new WaitForSeconds(.1f);
        transform.position = new Vector3(player.transform.position.x - 2.0f, player.transform.position.y, transform.position.z);
        yield return new WaitForSeconds(.1f);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.localScale *= 5f;
        audioEffects.clip = monsterJumpscare;
        audioEffects.Play();
        yield return new WaitForSeconds(.1f);
        enemySprite.enabled = false;
        yield return new WaitForSeconds(.1f);
        enemySprite.enabled = true;
        yield return new WaitForSeconds(.1f);
        enemySprite.enabled = false;
        yield return new WaitForSeconds(.1f);
        enemySprite.enabled = true;
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
