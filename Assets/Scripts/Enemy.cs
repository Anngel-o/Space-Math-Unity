using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public GameObject player;
    public GameObject bulletPrefab;
    public float shootingInterval = 2f;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    private float timer;

    public int multiplicand;
    public int multiplier;
    public int correctAnswer;
    public List<int> possibleAnswers;

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;

    public TMP_Text multiplicationTextTMP; // TextMeshPro Text

    void Start()
    {
        timer = shootingInterval;
        GenerateMultiplication();
        GeneratePossibleAnswers();
        UpdateMultiplicationUI();
    }

    void Update()
    {
        float playerX = player.transform.position.x;
        float enemyX = transform.position.x;

        float directionX = Mathf.Clamp(playerX - enemyX + 0.25f, -1f, 1f);
        transform.Translate(new Vector3(directionX, 0, 0) * speed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Shoot();
            timer = shootingInterval;
        }
    }

    void GenerateMultiplication()
    {
        multiplicand = Random.Range(1, 11);
        multiplier = Random.Range(1, 11);
        correctAnswer = multiplicand * multiplier;
    }

    void GeneratePossibleAnswers()
    {
        possibleAnswers = new List<int> { correctAnswer };
        while (possibleAnswers.Count < 3)
        {
            int wrongAnswer = Random.Range(1, 101);
            if (!possibleAnswers.Contains(wrongAnswer))
            {
                possibleAnswers.Add(wrongAnswer);
            }
        }
        Shuffle(possibleAnswers);
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void UpdateMultiplicationUI()
    {
        // Usa el objeto de texto referenciado directamente
        if (multiplicationTextTMP != null)
        {
            multiplicationTextTMP.text = $"{multiplicand} x {multiplier}";
        }
        else
        {
            Debug.LogError("MultiplicationTextTMP no asignado.");
        }

        // Verifica que los botones están asignados
        if (answerButton1 == null || answerButton2 == null || answerButton3 == null)
        {
            Debug.LogError("Botones de respuesta no asignados en el Inspector.");
            return;
        }

        Button[] buttons = { answerButton1, answerButton2, answerButton3 };
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
            {
                TMP_Text buttonText = buttons[i].GetComponentInChildren<TMP_Text>();
                if (buttonText != null)
                {
                    buttonText.text = possibleAnswers[i].ToString();
                }
                else
                {
                    Debug.LogError("El botón no tiene un componente TMP_Text.");
                }
                int index = i; // Para evitar el problema del cierre sobre la variable en el loop
                buttons[i].onClick.RemoveAllListeners(); // Limpiar los listeners anteriores
                buttons[i].onClick.AddListener(() => FindObjectOfType<Player>().SubmitAnswer(buttonText.text));
            }
            else
            {
                Debug.LogError($"Button {i + 1} no asignado.");
            }
        }
    }

    public float spawnPositionShootX = -0.5f;
    void Shoot()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, spawnPositionShootX, 0);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = Vector2.down * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }
}
