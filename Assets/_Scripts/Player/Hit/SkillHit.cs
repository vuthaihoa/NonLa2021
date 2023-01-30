using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHit : MonoBehaviour
{
    public float Speed;
    public float DestroyTime = 0.7f;
    public Rigidbody2D Rg;
    public GameObject hitParticle;
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    void Start()
    {
        Rg.velocity = transform.right * Speed;
        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
        if (enemy != null)
        {
            enemy.Takedamage(playerAttributesSO.damageHit);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Destroy(gameObject, DestroyTime);
        Boss_Health boss = hitInfo.GetComponent<Boss_Health>();
        if (boss != null)
        {
            boss.Takedamage(playerAttributesSO.damageHit);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
