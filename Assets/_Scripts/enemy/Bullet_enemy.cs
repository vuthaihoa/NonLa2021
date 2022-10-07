using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_enemy : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRg;
    public int Damage;
    public GameObject Hit;
    private void Start()
    {
        bulletRg = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRg.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
    //        Destroy(gameObject);
    //    }
    //    if(collision.gameObject.tag == "Ground")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
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
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Health>().Takedamage(Damage);
            Instantiate(Hit, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f)));
            Destroy(gameObject);
        }
    }
}
