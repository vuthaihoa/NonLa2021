using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rg;
    public GameObject hitParticle;
    void Start()
    {
        Rg.velocity = transform.right * Speed;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
        if(enemy != null)
        {
            enemy.Takedamage(PlayerStats.instance.Magic);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Destroy(gameObject,0.7f);
        Boss_Health boss = hitInfo.GetComponent<Boss_Health>();
        if (boss != null)
        {
            boss.Takedamage(PlayerStats.instance.Magic);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
