using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health;
    public GameObject bloodEffect;
    public GameObject deadParticle;
    public GameObject HitdeadParticle;
    public float dazedTime;
    public bool NoDamage = false;
    Animator ani;
    public OjbectDropItems ojbectDropItems;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void Takedamage(int damage)
    {
        if (NoDamage)
            return;
        dazedTime = 0.6f;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        ani.SetTrigger("injured");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 playerPos = player.transform.position;
        Vector2 pushDirection = new Vector2(transform.position.x, transform.position.y) - playerPos;
        pushDirection = pushDirection.normalized;
        rb.AddForce(pushDirection * 20f, ForceMode2D.Force);
        if (health <= 0)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Instantiate(HitdeadParticle, transform.position,Quaternion.identity);
            Destroy(gameObject);
            ojbectDropItems.DropItem();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TRAP")
        {
            Destroy(gameObject);
        }
    }
}
