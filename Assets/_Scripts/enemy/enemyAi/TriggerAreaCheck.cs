using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private EnemyAI EnemyParent;
    private void Awake()
    {
        EnemyParent = GetComponentInParent<EnemyAI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            EnemyParent.target = collision.transform;
            EnemyParent.inRange = true;
            EnemyParent.HotZone.SetActive(true);
        }
    }
}
