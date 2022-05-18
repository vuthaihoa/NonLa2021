using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack : MonoBehaviour
{
    public int Attack1;
    public Vector3 attackOffset1;
    public float attackRange1 = 1f;
    public LayerMask attackMask;
    public void Attack()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange1, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack1);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Gizmos.DrawWireSphere(poss, attackRange1);
    }
}

