using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_StoneCir : MonoBehaviour
{
    public float attackspeed;
    public float CanAttack;
    public int damage = 10;
    public float DestroyTime = 5f;
    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackspeed <= CanAttack)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                CanAttack = 0f;
            }
            else
            {
                CanAttack += Time.deltaTime;
            }
        }
        if (collision.gameObject.tag == "TRAP")
        {
            Destroy(gameObject);
        }
    }
}
