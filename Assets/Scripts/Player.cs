using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float velocity = 10f;
    Rigidbody2D rbPlayer;

    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float detectionRange = 5f;
    public Life life;
    public float damage;
    private Enemy enemyCurrentTarget;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(movement * velocity, rbPlayer.velocity.y);

        DetectEnemy();
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
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * shootForce;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void DetectEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                enemyCurrentTarget = hit.GetComponent<Enemy>();
                return;
            }
        }
        enemyCurrentTarget = null;
    }

    public void SubmitAnswer(string answer)
    {
        if (enemyCurrentTarget != null)
        {
            int parsedAnswer;
            if (int.TryParse(answer, out parsedAnswer) && parsedAnswer == enemyCurrentTarget.correctAnswer)
            {
                Shoot(enemyCurrentTarget.transform.position);
                enemyCurrentTarget = null;
            }
            else
            {
                Debug.Log("Respuesta incorrecta");
            }
        }
    }
}
