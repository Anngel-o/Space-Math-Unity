using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 10f;
    public float controlX, controlY;
    Rigidbody2D rbPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // float movement = Input.GetAxisRaw("Horizontal");
        // rbPlayer.velocity = new Vector2(movement * velocity, rbPlayer.velocity.y);

        // Comprueba si hay un toque en la pantalla
        if (Input.touchCount > 0)
        {
            // Obtén el primer toque en la pantalla
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    controlX = touchPosition.x - transform.position.x;
                    controlY = touchPosition.y - transform.position.y;
                    break;
                
                case TouchPhase.Moved:
                    rbPlayer.MovePosition(new Vector2(touchPosition.x - controlX, touchPosition.y - controlY));
                    break;

                case TouchPhase.Ended:
                    rbPlayer.velocity = Vector2.zero;
                    break;
            }
        }
    }
}
