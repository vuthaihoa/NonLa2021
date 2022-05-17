using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health;
    public GameObject bloodEffect;
    public GameObject deadParticle;
    public EnemyFollowPlayer EnemyFollowPlayer;
    public float dazedTime;
    Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        if (dazedTime <= 0)
        {
            EnemyFollowPlayer.Speed = 0.4f;
        }
        else
        {
            EnemyFollowPlayer.Speed = 0f;
            dazedTime -= Time.deltaTime;
        }
    }
    public void Takedamage(int damage)
    {
        dazedTime = 0.6f;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        ani.SetTrigger("injured");
        if (health <= 0)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
