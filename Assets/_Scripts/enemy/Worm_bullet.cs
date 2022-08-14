using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm_bullet : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float ShootingRange;
    public float nextFire = 2f;
    public float canFire = 10f;
    public GameObject bulletEnemy;
    public Transform bulletParent;
    private Transform player;
    bool faceingRight = false;
    //private float moveDirection;
    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
            ani.SetBool("run", true);
        }
        else
        {
            ani.SetBool("run", false);
        }
        if (distanceFromPlayer < ShootingRange)
        {
            if (canFire > nextFire)
            {
                canFire = 0f;
                ani.SetTrigger("attack");
                ani.SetBool("run", false);
            }
            else
            {
                canFire += Time.deltaTime;
            }
        }
        FlipTowardsPlayer();
    }
    public void Bullet()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= ShootingRange)
        {
            FindObjectOfType<AudioManager>().Play("EnemyBullet");
            Instantiate(bulletEnemy, bulletParent.position, bulletParent.rotation);
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
