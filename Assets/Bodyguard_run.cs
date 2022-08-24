using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodyguard_run : StateMachineBehaviour
{
    private int rand =0;
    public float range;
    public float speed;
    Transform player;
    Boss boss;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<Boss>();
        rb = animator.GetComponent<Rigidbody2D>();
        rand += 1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newpos);
        if (Vector2.Distance(player.position, rb.position) <= range)
        {
            if(rand == 1)
            {
                animator.SetTrigger("skill1");
            }
            if (rand == 2)
            {
                animator.SetTrigger("skill2");
            }
            if (rand == 3)
            {
                animator.SetTrigger("skill3");
            }
            if (rand == 4)
            {
                animator.SetTrigger("skill4");
                rand = 0;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("skill1");
        animator.ResetTrigger("skill2");
        animator.ResetTrigger("skill3");
        animator.ResetTrigger("skill4");
    }
}
