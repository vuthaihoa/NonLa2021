using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfsitePlayer;
    public float StopMove;
    private Transform player;

    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        followPlayer();
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
    }
}
