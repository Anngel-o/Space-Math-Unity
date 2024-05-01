using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 10f;
    Rigidbody2D rbPlayer;

    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float detectionRange = 5f;
    public Life life;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(movement * velocity, rbPlayer.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MisilEnemy"))
        {
            life.currentLife -= damage;
            Destroy(other.gameObject);
        }
    }

    public void Shoot(Vector2 targetPosition)
    {
        // Instanciar proyectil
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        // Calcular dirección hacia el objetivo
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        
        // Aplicar fuerza al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * shootForce;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un círculo para visualizar el rango de detección
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
