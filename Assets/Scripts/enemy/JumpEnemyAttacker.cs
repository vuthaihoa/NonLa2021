using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyAttacker : MonoBehaviour
{
    [Header("For Petrolling")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform GroundCheckPoint;
    [SerializeField] Transform WallCheckPoint;
    [SerializeField] float CircleRadius;
    [SerializeField] LayerMask groundPlayer;
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
    private bool canSeePlayer;

    [Header("Other")]
    private Animator enemAni;
    private Rigidbody2D enemyRB;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemAni = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        CheckingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, CircleRadius, groundPlayer);
        CheckingWall = Physics2D.OverlapCircle(WallCheckPoint.position, CircleRadius, groundPlayer);
        isGround = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundPlayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSite, 0, playerayer);
        AnimationController();
        if (!canSeePlayer && isGround)
        {
            Petrolling();
        }
    }
    void Petrolling()
    {
        if(!CheckingGround || CheckingWall)
        {
            if(facingRight)
            {
                flip();
            }
            else if(!facingRight)
            {
                flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }
    void JumpAttack()
    {
        float DistanceFromPlayer = player.position.x - transform.position.x;
        if(isGround)
        {
            enemyRB.AddForce(new Vector2(DistanceFromPlayer, jumpheight), ForceMode2D.Impulse);
            FindObjectOfType<AudioManager>().Play("EnemyJump");
        }

    }
    void flipTowardsOlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if(playerPosition < 0 && facingRight)
        {
            flip();
        }
        else if(playerPosition > 0 && !facingRight)
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
        Gizmos.DrawWireSphere(GroundCheckPoint.position, CircleRadius);
        Gizmos.DrawWireSphere(WallCheckPoint.position, CircleRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheck.position, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSite);
    }
}
