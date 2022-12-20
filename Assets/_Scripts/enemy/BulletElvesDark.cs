using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletElvesDark : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float ShootingRange;
    public float nextFire = 2f;
    public float canFire = 10f;
    public GameObject bulletEnemy;
    public GameObject bulletParent;
    private Transform player;
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
            if (canFire > nextFire)
            {
                canFire = 0f;
                ani.SetTrigger("attack");
            }
            else
            {
                canFire += Time.deltaTime;
            }
        }
    }
    public void Bullet()
    {
        //float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //if (distanceFromPlayer <= ShootingRange)
        //{

        //}
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(bulletEnemy, bulletParent.transform.position, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }
}
