using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float DashRange;
    public float startDash;
    public float nextDash;
    private Transform player;
    bool faceingRight = false;
    private float moveDirection;

    private bool CanDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime = 0.2f;

    Rigidbody2D Rg;
    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > DashRange)
        {
            ani.SetBool("run", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }
        else
        {
            ani.SetBool("run", false);
        }
        if(distanceFromPlayer < DashRange)
        {
            if (nextDash < startDash && CanDash)
            {
                startDash = 0f;
                ani.SetTrigger("dash");
                ani.SetBool("run", false);
            }
            else
            {
                startDash += Time.deltaTime;
            }
        }
        FlipTowardsPlayer();
    }
    public IEnumerator Dash()
    {
        CanDash = false;
        isDashing = true;
        float originalGravity = Rg.gravityScale;
        Rg.gravityScale = 0;
        if (faceingRight)
        {
            Rg.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else
        {
            Rg.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        Rg.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(0.3f);
        CanDash = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, DashRange);
    }
    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if (playerPosition < 0 && faceingRight)
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
}
