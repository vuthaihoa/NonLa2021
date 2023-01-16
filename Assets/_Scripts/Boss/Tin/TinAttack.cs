using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinAttack : MonoBehaviour
{
    [Header("dash")]
    public float dashingPower;
    public float dashingTime = 0.2f;
    public GameObject damaColli;

    private Transform player;

    Rigidbody2D Rg;
    Animator ani;

    public int Attack1;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    [Header("Speard")]
    public Transform Bullet_parents;
    public GameObject bulletTin;
    [Header("health")]
    public Enemy_Health enemy_Health;
    private Boss boss;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boss = GetComponent<Boss>();
    }
    void Update()
    {
        if (enemy_Health.health <= 300)
        {
            ani.SetBool("angry", true);
            FindObjectOfType<AudioManager>().Play("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        }
    }
    public IEnumerator Dash()
    {
        float originalGravity = Rg.gravityScale;
        Rg.gravityScale = 0;
        if (boss.isFipped)
        {
            Rg.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            FindObjectOfType<AudioManager>().Play("Dash_Enemy");
        }
        else
        {
            Rg.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
            FindObjectOfType<AudioManager>().Play("Dash_Enemy");
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (boss.isFipped)
        {
            rb.AddForce(-transform.position * 20f, ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(transform.position * 20f, ForceMode2D.Force);
        }
    }
    public void Bullet()
    {
        Instantiate(bulletTin, Bullet_parents.position, Bullet_parents.rotation);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
    public void BoxCollDashOn()
    {
        damaColli.SetActive(true);
    }
    public void BoxCollDashOff()
    {
        damaColli.SetActive(false);
    }
}
