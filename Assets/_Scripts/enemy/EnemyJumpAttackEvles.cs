using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpAttackEvles : MonoBehaviour
{
    [Header("For Petrolling")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform GroundCheckPoint;
    [SerializeField] Transform WallCheckPoint;
    [SerializeField] float CircleRadius;
    [SerializeField] float CircleRadius1;
    [SerializeField] LayerMask groundPlayer;
    [SerializeField] LayerMask EnemyCheck;
    private bool CheckingGround;
    private bool CheckingWall;

    [Header("For JumpAttacking")]
    [SerializeField] float jumpheight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    private bool isGround;

    [Header("For SeeingPlayer")]
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask playerayer;
    [SerializeField] Transform seePlayer;
    private bool canSeePlayer;

    [Header("Other")]
    private Animator enemAni;
    private Rigidbody2D enemyRB;

    [Header("Damage")]
    [SerializeField] int Attack2;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemAni = GetComponent<Animator>();
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }
    void FixedUpdate()
    {
        CheckingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, CircleRadius1, groundPlayer);
        if(CheckingWall = Physics2D.OverlapCircle(WallCheckPoint.position, CircleRadius, groundPlayer))
        {

        }
        else
        {
            CheckingWall = Physics2D.OverlapCircle(WallCheckPoint.position, CircleRadius, EnemyCheck);
        }
        isGround = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundPlayer);
        canSeePlayer = Physics2D.OverlapBox(seePlayer.position, lineOfSite, 0, playerayer);
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0.9f)
        {
            canSeePlayer = Physics2D.OverlapBox(seePlayer.position, lineOfSite, 0, playerayer);
        }
        else
        {
            canSeePlayer = false;
        }
        AnimationController();
        if (!canSeePlayer && isGround)
        {
            if (CheckingWall)
            {
                StartCoroutine(WaitAndContinue());
            }
            else
            {
                Petrolling();
            }
        }
    }
    void Petrolling()
    {
        if (!CheckingGround || CheckingWall)
        {
            if (facingRight)
            {
                flip();
            }
            else if (!facingRight)
            {
                flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }
    private IEnumerator WaitAndContinue()
    {
        yield return new WaitForSeconds(3);
        Petrolling();
    }
    void JumpAttack()
    {
        float DistanceFromPlayer = player.position.x - transform.position.x;
        if (isGround)
        {
            enemyRB.AddForce(new Vector2(DistanceFromPlayer, jumpheight), ForceMode2D.Impulse);
            FindObjectOfType<AudioManager>().Play("MT_skill1");
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
    void flipTowardsOlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if (playerPosition < 0 && facingRight)
        {
            flip();
        }
        else if (playerPosition > 0 && !facingRight)
        {
            flip();
        }
    }
    void flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    void AnimationController()
    {
        enemAni.SetBool("canSeePlayer", canSeePlayer);
        enemAni.SetBool("isGround", isGround);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheckPoint.position, CircleRadius1);
        Gizmos.DrawWireSphere(WallCheckPoint.position, CircleRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheck.position, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(seePlayer.position, lineOfSite);

        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
