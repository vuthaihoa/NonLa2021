using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyguardDamage : MonoBehaviour
{
    [Header("dash")]
    public float dashingPower;
    public float dashingTime = 0.2f;

    private Transform player;
    bool faceingRight = false;

    Rigidbody2D Rg;

    public int Attack1;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    [Header("Speard")]
    public GameObject Spear_parents;
    public GameObject Spear_parents1;
    public GameObject Spear_parents2;
    public GameObject Spear_parents3;
    public GameObject Spear_parents4;
    public Transform Spear;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Rg = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        FlipTowardsPlayer();
    }
    public IEnumerator Dash()
    {
        float originalGravity = Rg.gravityScale;
        Rg.gravityScale = 0;
        if (!faceingRight)
        {
            Rg.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else
        {
            Rg.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        Rg.gravityScale = originalGravity;
        yield return new WaitForSeconds(0.3f);
    }
    public void Attack()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset.x;
        poss += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange, attackMask);
        FindObjectOfType<AudioManager>().Play("MT_skill1");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack1);
        }
    }
    public void SpeardOn()
    {
        Instantiate(Spear, Spear_parents.transform.position, Quaternion.identity);
        Instantiate(Spear, Spear_parents1.transform.position, Quaternion.identity);
        Instantiate(Spear, Spear_parents2.transform.position, Quaternion.identity);
        Instantiate(Spear, Spear_parents3.transform.position, Quaternion.identity);
        Instantiate(Spear, Spear_parents4.transform.position, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
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
        transform.Rotate(0f, 0f, 0f);
    }
}
