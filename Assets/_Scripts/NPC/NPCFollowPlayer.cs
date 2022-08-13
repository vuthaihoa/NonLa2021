using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfsitePlayer;
    public float StopMove;
    public float TelePortNPC;
    private Transform player;
    bool FaceingRight = true;
    private float MoveDiretion;

    public float AttackZone;
    public float nextAttack;

    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        followPlayer();
        TelePort();
        FlipTowardsPlayer();
        AttackZone += Time.deltaTime;
    }
    void followPlayer()
    {
        float follow = Vector2.Distance(player.position, transform.position);
        if (follow < lineOfsitePlayer && follow > StopMove)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            ani.SetBool("speed", true);
        }
        else
        {
            ani.SetBool("speed", false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfsitePlayer);
        Gizmos.DrawWireSphere(transform.position, StopMove);
        Gizmos.DrawWireSphere(transform.position, TelePortNPC);
    }
    void TelePort()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        if (distance > TelePortNPC)
        {
            Vector3 playerPosRandomized = player.transform.position;
            playerPosRandomized.x = playerPosRandomized.x + UnityEngine.Random.Range(0f, 1f);
            playerPosRandomized.z = playerPosRandomized.z + UnityEngine.Random.Range(-10f, 30f);
            playerPosRandomized.y = playerPosRandomized.y + UnityEngine.Random.Range(0f, 0f);
            transform.position = playerPosRandomized;
        }
    }
    void FlipTowardsPlayer()
    {
        float PlayerPosition = player.position.x - transform.position.x;
        if (PlayerPosition < 0 && FaceingRight)
        {
            Flip();
        }
        else if (PlayerPosition > 0 && !FaceingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        MoveDiretion *= -1;
        FaceingRight = !FaceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(AttackZone > nextAttack)
            {
                ani.SetTrigger("attack");
                AttackZone = 0f;
            }
            else
            {
                AttackZone += Time.deltaTime;
            }
        }
    }
}
