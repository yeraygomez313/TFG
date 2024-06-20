using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float leftLimit; // L�mite izquierdo de la patrulla en el eje X
    public float rightLimit; // L�mite derecho de la patrulla en el eje X
    public float speed = 2f; // Velocidad de movimiento del enemigo

    private SpriteRenderer enemySprite;

    private bool movingRight = true; // Bandera para la direcci�n de movimiento
    private bool isUnderLight = false; // Bandera para indicar si est� bajo el efecto de una luz

    private void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
    }

    void Update()
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
            // Aqu� puedes implementar la l�gica para el comportamiento del enemigo al colisionar con el jugador
            print("Enemy collided with player.");
            // Por ejemplo, podr�as restarle vida al jugador, mostrar un efecto, etc.
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
}
