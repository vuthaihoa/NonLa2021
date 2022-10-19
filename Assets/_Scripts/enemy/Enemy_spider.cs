using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spider : MonoBehaviour
{
    public float lineOfSite;
    public float nextFire = 2f;
    public float canFire = 10f;
    public GameObject bulletEnemy;
    public GameObject bulletParent;
    private Transform player;
    bool faceingRight = true;
    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
            ani.SetBool("run", true);
            Roration(player);
        }
        else
        {
            ani.SetBool("run", false);
        }
    }
    public void Bullet()
    {
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(bulletEnemy, bulletParent.transform.position, Quaternion.identity);
    }
    private void Roration(Transform target)
    {
        if (faceingRight == true)
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 40 * Time.deltaTime);
            if (transform.rotation == targetRotation)
            {
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + 2f;
                    ani.SetTrigger("attack");
                }

            }
        }
        else
        {
            float angle = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 40 * Time.deltaTime);
            if (transform.rotation == targetRotation)
            {
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + 2f;
                    ani.SetTrigger("attack");
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
    void flip()
    {
        faceingRight = !faceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
