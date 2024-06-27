using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    // Referencia al objeto del jugador
    public Transform player;
    // Velocidad de movimiento del enemigo
    public float speed = 5f;

    void Update()
    {
        // Calcular la dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Mover el enemigo hacia el jugador
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobar si el enemigo ha entrado en el área de la luz
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(6);
        }
    }
}
