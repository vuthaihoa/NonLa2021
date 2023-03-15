using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoLaoAttack : MonoBehaviour
{
    [Header("BulletBoiLao")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletParents;
    [SerializeField] private GameObject Bullet2;
    [SerializeField] private Transform BulletParents2;
    [SerializeField] private Transform TreeTrapParents;
    [SerializeField] private GameObject treeTrap;
    [Header("attack")]
    [SerializeField] private int Attack1;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int Attack2;
    [SerializeField] private Vector3 attackOffset2;
    [SerializeField] private float attackRange2 = 1f;
    [SerializeField] private LayerMask attackMask;
    [Header("lotusFlower")]
    [SerializeField] private GameObject LotusFlower;
    [Header("Cutscene")]
    [SerializeField] private GameObject SaveGao;
    [SerializeField] private int HpCutscene;
    private Enemy_Health enemy_Health;
    private void Start()
    {
        enemy_Health= GetComponent<Enemy_Health>();
    }
    private void Update()
    {
        if(enemy_Health.health < HpCutscene)
        {
            SaveGao.SetActive(true);
        }
    }
    public void Attack()
    {
        Vector3 poss = transform.position;
        poss += transform.right * attackOffset.x;
        poss += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(poss, attackRange, attackMask);
        FindObjectOfType<AudioManager>().Play("MT_skill1");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack1);
        }
    }
    public void Attack_2()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset2.x;
        pos += transform.up * attackOffset2.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange2, attackMask);
        FindObjectOfType<AudioManager>().Play("ThuongLuongAngry");
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage(Attack2);
        }
    }
    public void Bullet1()
    {
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(Bullet, BulletParents.transform.position, Quaternion.identity);
    }
    public void BulletL()
    {
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(Bullet2, BulletParents2.transform.position, Quaternion.identity);
    }
    public void TreeTrap()
    {
        Instantiate(treeTrap, TreeTrapParents.transform.position, Quaternion.identity);
    }
    public void lotusOn()
    {
        LotusFlower.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ThuongLuongAngry");
    }
    public void lotusOff()
    {
        LotusFlower.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);

        pos += transform.right * attackOffset2.x;
        pos += transform.up * attackOffset2.y;
        Gizmos.DrawWireSphere(pos, attackRange2);
    }
}
