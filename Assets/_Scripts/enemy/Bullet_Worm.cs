using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Worm : MonoBehaviour
{
    public float speed;
    public int Damage;
    public Rigidbody2D Rb;
    //public GameObject Ex_Worm;
    void Start()
    {
        Rb.velocity = transform.right * speed;
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
        Destroy(gameObject,1);
    }
}
