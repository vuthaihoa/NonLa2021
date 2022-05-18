using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float ShootingRange;
    public float fireRate;
    public GameObject bulletEnemy;
    public GameObject bulletParent;
    private Transform player;
    bool faceingRight = false;
    private float moveDirection;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }
        FlipTowardsPlayer();
    }
    void Bullet()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= ShootingRange)
        {
            FindObjectOfType<AudioManager>().Play("EnemyBullet");
            Instantiate(bulletEnemy, bulletParent.transform.position, Quaternion.identity);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }
    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if(playerPosition<0 && faceingRight)
        {
            flip();
        }
        if (playerPosition > 0 && !faceingRight)
        {
            flip();
        }
    }
    void flip()
    {
        moveDirection *= -1;
        faceingRight = !faceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
