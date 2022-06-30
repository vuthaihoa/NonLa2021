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
        if (health <= 0)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Instantiate(HitdeadParticle, transform.position,Quaternion.identity);
            Destroy(gameObject);
            ojbectDropItems.DropItem();
        }
    }
}
