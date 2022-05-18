using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private EnemyAI EnemyParent;
    private bool inRange;
    private Animator ani;
    private void Awake()
    {
        EnemyParent = GetComponentInParent<EnemyAI>();
        ani = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (inRange && !ani.GetCurrentAnimatorStateInfo(0).IsName("Wolf_attack"))
        {
            EnemyParent.flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            EnemyParent.triggerArea.SetActive(true);
            EnemyParent.inRange = false;
            EnemyParent.SelectTarget();
        }
    }
}
