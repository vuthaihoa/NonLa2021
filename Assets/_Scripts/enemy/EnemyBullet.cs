using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float ShootingRange;
    public float nextFire = 2f;
    public float canFire = 10f;
    public GameObject bulletEnemy;
    public GameObject bulletParent;
    private Transform player;
    bool faceingRight = false;
    private float moveDirection;
    Animator ani;
    [Header("Damage")]
    [SerializeField] int Attack2;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;
    [SerializeField] private int Attack1;
    [SerializeField] private Vector3 attackOffset1;
    [SerializeField] private float attackRange1 = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
            //ani.SetBool("run", true);
        }
        else
        {
            //ani.SetBool("run", false);
        }
        if (distanceFromPlayer < ShootingRange)
        {
            if(canFire > nextFire)
            {
                canFire = 0f;
                ani.SetTrigger("attack");
                //ani.SetBool("run", false);
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
            Instantiate(bulletEnemy, bulletParent.transform.position, Quaternion.identity);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);

        Gizmos.color = Color.white;
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);

        Gizmos.color = Color.red;
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Gizmos.DrawWireSphere(poss, attackRange1);
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
    public void Attack_2()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        FindObjectOfType<AudioManager>().Play("MT_skill1");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack2);
        }
    }
    public void Fire()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange1, attackMask);
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack1);
        }
    }
}
