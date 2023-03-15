using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int DamageAttack;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public void Attack()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset.x;
        poss += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(DamageAttack);
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
