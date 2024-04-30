using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 10f;
    public float controlX, controlY;
    Rigidbody2D rbPlayer;

    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float detectionRange = 5f;
    public string targetTag = "Enemy";
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

        // // Detección de objetos y disparo automático
        // Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        
        // foreach (Collider2D hitCollider in hitColliders)
        // {
        //     // Si el objeto tiene la etiqueta deseada, dispara
        //     if (hitCollider.CompareTag(targetTag))
        //     {
        //         Shoot(hitCollider.transform.position);
        //     }
        // }


        // // Comprueba si hay un toque en la pantalla
        // if (Input.touchCount > 0)
        // {
        //     // Obtén el primer toque en la pantalla
        //     Touch touch = Input.GetTouch(0);
        //     Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        //     switch (touch.phase)
        //     {
        //         case TouchPhase.Began:
        //             controlX = touchPosition.x - transform.position.x;
        //             controlY = touchPosition.y - transform.position.y;
        //             break;
                
        //         case TouchPhase.Moved:
        //             rbPlayer.MovePosition(new Vector2(touchPosition.x - controlX, touchPosition.y - controlY));
        //             break;

        //         case TouchPhase.Ended:
        //             rbPlayer.velocity = Vector2.zero;
        //             break;
        //     }
        // }
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
