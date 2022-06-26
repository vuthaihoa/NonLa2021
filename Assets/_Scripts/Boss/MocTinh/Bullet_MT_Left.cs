using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_MT_Left : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public int Damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 rotation = transform.eulerAngles;
        rotation.y = 180;
        if (rotation.y == 180)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            Destroy(this.gameObject, 2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}