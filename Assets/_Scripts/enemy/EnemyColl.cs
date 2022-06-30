using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColl : MonoBehaviour
{
    public float attackspeed;
    public float CanAttack;
    public float Speed;
    public float lineOfSite;
    private Transform player;
    public int damage = 10;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }
        CanAttack += Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
    private void OnTriggerStay2D(Collider2D collision)
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
    }
}
