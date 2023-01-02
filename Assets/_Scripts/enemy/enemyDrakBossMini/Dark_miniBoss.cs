using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Dark_miniBoss : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float AttackingRange;
    public float nextFire = 2f;
    public float canFire = 10f;
    private Transform player;
    bool faceingRight = false;
    public int Attack1;
    public Vector3 attackOffset1;
    public float attackRange1 = 1f;
    public int Attack2;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    int rand;
    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > AttackingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
            ani.SetBool("run", true);
        }
        if (distanceFromPlayer < AttackingRange)
        {
            if (canFire > nextFire)
            {
                canFire = 0f;
                rand = Random.Range(1, 4);
                if(rand >= 3)
                {
                    ani.SetTrigger("attack2");
                }
                else
                {
                    ani.SetTrigger("attack1");
                }
            }
            else
            {
                rand = 0;
                canFire += Time.deltaTime;
            }
            ani.SetBool("run", false);
        }
        FlipTowardsPlayer();
    }
    public void Attack()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange1, attackMask);
        FindObjectOfType<AudioManager>().Play("MT_skill1");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack1);
        }
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, AttackingRange);

        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);

        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Gizmos.DrawWireSphere(poss, attackRange1);
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
        faceingRight = !faceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
