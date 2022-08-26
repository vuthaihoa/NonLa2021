using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    Rigidbody2D rb;
    public float gravity;
    public int damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
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
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        Destroy(gameObject, 1.5f);
    }
}
