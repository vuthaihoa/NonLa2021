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
        if (colInfo != null)
        {
            FindObjectOfType<AudioManager>().Play("wolfAttack");
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
}
