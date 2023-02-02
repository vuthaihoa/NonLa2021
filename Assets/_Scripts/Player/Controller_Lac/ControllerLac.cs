using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLac : MonoBehaviour
{
    Rigidbody2D Rb;
    [Header("Horizotal")]
    [SerializeField] private float Maxspeed;
    private Vector2 moveDirection;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        ProcessInput();
    }
    private void FixedUpdate()
    {
        move();
    }
    void ProcessInput()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        moveDirection = new Vector2(movex, movey).normalized;
    }
    void move()
    {
        Rb.velocity = new Vector2(moveDirection.x * Maxspeed, moveDirection.y * Maxspeed);
    }
}
