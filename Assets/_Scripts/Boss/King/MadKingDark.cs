using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadKingDark : MonoBehaviour
{
    public int Attack1;
    public int Attack2;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public Vector3 attackOffset1;
    public float attackRange1 = 1f;
    public LayerMask attackMask;
    public Enemy_Health Enemy_Health;
    public GameObject CutsceneDeadMom;
    public void Attack()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange1, attackMask);
        FindObjectOfType<AudioManager>().Play("MT_skill1");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack1);
        }
    }
    public void Attack_2()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        FindObjectOfType<AudioManager>().Play("MT_skill1");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack2);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);

        Vector3 poss = transform.position;
        poss += transform.right * attackOffset1.x;
        poss += transform.up * attackOffset1.y;
        Gizmos.DrawWireSphere(poss, attackRange1);
    }
}
