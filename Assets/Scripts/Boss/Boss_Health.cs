using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    public bool isInvulnerable = false;
    public int Angry;
    private void Update()
    {
        if (health <= Angry)
        {
            GetComponent<Animator>().SetBool("isAngry", true);
        }
        if (health <= 0)
        {
            die();
        }
    }
    public void Takedamage(int damage)
    {
        if(isInvulnerable)
            return;
        health -= damage;
    }
    void die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
