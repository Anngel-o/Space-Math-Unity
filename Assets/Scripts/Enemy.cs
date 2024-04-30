using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento horizontal de la nave enemiga
    public GameObject player; // Referencia al objeto del jugador
    public GameObject bulletPrefab; // Prefab del proyectil
    public float shootingInterval = 2f; // Intervalo de tiempo entre disparos
    public float bulletSpeed = 10f; // Velocidad del proyectil
    public float bulletLifetime = 2f; // Tiempo de vida del proyectil

    private float timer; // Temporizador para controlar el disparo

    void Start()
    {
        timer = shootingInterval; // Iniciar el temporizador
    }

    void Update()
    {
        // Obtén las coordenadas horizontales del jugador y de la nave enemiga
        float playerX = player.transform.position.x;
        float enemyX = transform.position.x;

        // Calcula la dirección hacia la que la nave enemiga debe moverse para seguir al jugador
        float directionX = Mathf.Clamp(playerX - enemyX + 0.25f, -1f, 1f);

        // Mueve la nave enemiga horizontalmente
        transform.Translate(new Vector3(directionX, 0, 0) * speed * Time.deltaTime);

        // Disparo
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Shoot(); // Llama a la función de disparo
            timer = shootingInterval; // Reinicia el temporizador
        }
    }

    void Shoot()
    {
        // Crea un nuevo proyectil en la posición de la nave enemiga
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Agrega una velocidad hacia abajo al proyectil
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = Vector2.down * bulletSpeed;

        // Destruye el proyectil después de un cierto tiempo para liberar memoria
        Destroy(bullet, bulletLifetime);
    }
}
