using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    public int AttackNPC;
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
            colInfo.GetComponent<Enemy_Health>().Takedamage(AttackNPC);
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
