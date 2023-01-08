using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHPUpgrade : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rg;
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    void Start()
    {
        Rg.velocity = transform.right * Speed;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
        if (enemy != null)
        {
            enemy.Takedamage(playerAttributesSO.Magic);
        }
        Boss_Health boss = hitInfo.GetComponent<Boss_Health>();
        if (boss != null)
        {
            boss.Takedamage(playerAttributesSO.Magic);
        }

    }
}
