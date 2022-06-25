using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocTinh : MonoBehaviour
{
    public GameObject bulletEnemyRight;
    public Transform bulletParentRgiht;
    public Transform bulletParentLeft;
    public GameObject bulletEnemyLeft;

    public int Attack1;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    Transform player;
    Rigidbody2D rb;
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MT_debut");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    public void Bullet()
    {
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(bulletEnemyRight, bulletParentRgiht.transform.position, Quaternion.identity);
        Instantiate(bulletEnemyLeft, bulletParentLeft.transform.position, Quaternion.identity);
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
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            float distance = Vector2.Distance(player.transform.position, rb.transform.position);
            Vector2 playerPosRandomized = player.transform.position;
            playerPosRandomized.x = playerPosRandomized.x + UnityEngine.Random.Range(-2f, 2f);
            playerPosRandomized.y = playerPosRandomized.y + UnityEngine.Random.Range(0f, 0f);
            rb.transform.position = playerPosRandomized;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            float distance = Vector2.Distance(player.transform.position, rb.transform.position);
            Vector2 playerPosRandomized = player.transform.position;
            playerPosRandomized.x = playerPosRandomized.x + UnityEngine.Random.Range(-2f, 2f);
            playerPosRandomized.y = playerPosRandomized.y + UnityEngine.Random.Range(0f, 0f);
            rb.transform.position = playerPosRandomized;
            FindObjectOfType<AudioManager>().Play("MT_debut");
        }
    }
}
