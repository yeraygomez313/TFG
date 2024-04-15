using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float speed = 5f; // Velocidad de movimiento del enemigo
    public float jumpForce = 10f; // Fuerza del salto del enemigo
    public LayerMask groundLayer; // Capa que representa el suelo
    public float groundCheckRadius = 0.1f; // Radio del círculo de comprobación del suelo
    private bool isGrounded;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Calculamos la dirección hacia la que debe moverse el enemigo para perseguir al jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Movemos al enemigo en la dirección del jugador con una velocidad constante
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        print(isGrounded + "  grounddd");
        // Si el enemigo está en el suelo y el jugador está por encima, saltamos
        if (isGrounded && player.position.y > transform.position.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que entra en contacto es el jugador
        if (other.CompareTag("Player"))
        {
            // Printeamos una frase
            Debug.Log("¡El enemigo te ha atrapado!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
