using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUpgrade : MonoBehaviour
{
    private GameObject[] target;
    public float speed;
    public GameObject hitParticle;
    public float DestroyTime = 0.7f;
    public float RorationTimne;
    Rigidbody2D bulletRg;
    private Transform enemy;
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        bulletRg = GetComponent<Rigidbody2D>();

        FindEnemy();
    }
    private void Update()
    {
        if (Time.time >= RorationTimne)
        {
            Roration(enemy);
            RorationTimne = Time.time + 2f;
        }
    }
    private void Roration(Transform target)
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200);
    }
    public Transform FindEnemy()
    {
        target = GameObject.FindGameObjectsWithTag("Enemy");
        float distanceToClosestEnemy = Mathf.Infinity;
        Transform trans = null;
        foreach(GameObject go in target)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = currentDistance;
                trans = go.transform;
                enemy = go.transform;
                Vector2 moveDir = (trans.transform.position - transform.position).normalized * speed;
                bulletRg.velocity = new Vector2(moveDir.x, moveDir.y);
            }
        }
        return trans;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
        if (enemy != null)
        {
            enemy.Takedamage(playerAttributesSO.Magic);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Destroy(gameObject, DestroyTime);
        Boss_Health boss = hitInfo.GetComponent<Boss_Health>();
        if (boss != null)
        {
            boss.Takedamage(playerAttributesSO.Magic);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
