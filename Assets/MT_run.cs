using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MT_run : StateMachineBehaviour
{
    private int rand;
    public float range;
    public float speed;
    Transform player;
    Boss boss;
    Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<Boss>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newpos);
        rand = Random.Range(5,17);
        if(Vector2.Distance(player.position, rb.position) <= range)
        {
            if(rand <=5 )
            {
                animator.SetTrigger("skill1");
            }
            else if(rand >=12 )
            {
                animator.SetTrigger("skill2");
            }
            else
            {
                animator.SetTrigger("invisible");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("skill1");
        animator.ResetTrigger("skill2");
    }
}
