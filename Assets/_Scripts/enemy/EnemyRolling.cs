using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRolling : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float RollRange;
    public float startRoll;
    public float nextRoll;
    private Transform player;
    bool faceingRight = false;
    private float moveDirection;

    Rigidbody2D Rg;
    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > RollRange)
        {
            Rolling();
        }
        else
        {
            ani.SetBool("attack", false);
        }
        FlipTowardsPlayer();
    }
    public void Rolling()
    {
        if (nextRoll < startRoll)
        {
            ani.SetBool("attack", true);
            startRoll -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }
        if(startRoll <= 3)
        {
            startRoll -= Time.deltaTime;
            ani.SetBool("attack", false);
            if (startRoll <= 0)
            {
                startRoll = 6;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if (playerPosition > 0 && faceingRight)
        {
            flip();
        }
        if (playerPosition < 0 && !faceingRight)
        {
            flip();
        }
    }
    void flip()
    {
        //moveDirection *= -1;
        faceingRight = !faceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
