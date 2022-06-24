using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MT_tele : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float tele;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(player.transform.position, rb.transform.position);
        if (distance <= tele)
        {
            Vector2 playerPosRandomized = player.transform.position;
            playerPosRandomized.x = playerPosRandomized.x + UnityEngine.Random.Range(-2f, 2f);
            playerPosRandomized.y = playerPosRandomized.y + UnityEngine.Random.Range(0f, 0f);
            rb.transform.position = playerPosRandomized;
        }
        else
        {
            Vector2 playerPosRandomized = player.transform.position;
            playerPosRandomized.x = playerPosRandomized.x + UnityEngine.Random.Range(-2f, 2f);
            playerPosRandomized.y = playerPosRandomized.y + UnityEngine.Random.Range(0f, 0f);
            rb.transform.position = playerPosRandomized;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
