using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 10.0f;
    public float minX = -4.0f; // Valor mínimo de X
    public float maxX = 4.0f;  // Valor máximo de X

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var vel = rb2d.velocity;

        if (Input.GetKey(moveLeft) && transform.position.x > minX)
        {
            vel.x = -speed;
        }
        else if (Input.GetKey(moveRight) && transform.position.x < maxX)
        {
            vel.x = speed;
        }
        else
        {
            vel.x = 0;
        }

        rb2d.velocity = vel;
    }
}
