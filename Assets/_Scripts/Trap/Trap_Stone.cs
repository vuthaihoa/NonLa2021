using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Stone : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;
    public float gravity;
    public int damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        rb.gravityScale = 0f;
    }
    void Update()
    {

    }
    public void SpearOn()
    {
        rb.gravityScale = gravity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetTrigger("Down");
            FindObjectOfType<AudioManager>().Play("Golem");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetTrigger("Down");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
