using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Worm : MonoBehaviour
{
    public float speed;
    public int Damage;
    public Rigidbody2D Rb;
    public GameObject Hit;
    void Start()
    {
        Rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Health>().Takedamage(Damage);
            Instantiate(Hit, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f)));
            Destroy(gameObject);
        }
        Destroy(gameObject,1);
    }
}
