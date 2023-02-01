using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    Animator ani;
    public AudioSource audio_S;
    public AudioClip[] sonido;

    public int combo;
    public bool atacando;
    public bool die = true;

    PlayerController player;
    //float NextAttackTime = 0f;
    //public float StartTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatEnemies;
    public float attackRange;
    public GameObject hitParticle;
    public Transform hitUpgradeparents;
    public GameObject hitUpgrade;
    public GameObject hitUpgrade2;
    public GameObject hitUpgrade3;
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    void Start()
    {
        ani = GetComponent<Animator>();
        audio_S = GetComponent<AudioSource>();
        player = GetComponent<PlayerController>();
    }
    public void Start_Combo()
    {
        atacando = false;
        if (combo < 3)
        {
            combo++;
        }
    }
    public void Finish_Ani()
    {
        combo = 0;
        atacando = false;
    }
    public void Combo_()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !atacando && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            atacando = true;
            ani.SetTrigger("" + combo);
            audio_S.clip = sonido[combo];
            audio_S.Play();
            player.hide = atacando;
            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //if(player.facingRight)
            //{
            //    rb.AddForce(-transform.position * 20f, ForceMode2D.Force);
            //}
            //else
            //{
            //    rb.AddForce(transform.position * 20f, ForceMode2D.Force);
            //}
        }
    }
    void Update()
    {
        if(die)
        {
            Combo_();
            //if (Time.time >= NextAttackTime)
            //{
            //        Combo_();
            //}
        }
        if(PlayerStats.instance.currentHealth <= 0)
        {
            die = false;
        }
    }
    public void DamageCombo()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy_Health>().Takedamage(playerAttributesSO.damage);
            Instantiate(hitParticle, attackPos.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            //NextAttackTime = Time.time + 1f / StartTimeBtwAttack;
        }
    }
    public void HitUpgrade()
    {
        if(playerAttributesSO.damage >= 30)
        {
            Instantiate(hitUpgrade, hitUpgradeparents.position, hitUpgradeparents.rotation);
        }
    }
    public void HitUpgrade2()
    {
        if (playerAttributesSO.damage >= 40)
        {
            Instantiate(hitUpgrade2, hitUpgradeparents.position, hitUpgradeparents.rotation);
        }
    }
    public void HitUpgrade3()
    {
        if (playerAttributesSO.damage >= 50)
        {
            Instantiate(hitUpgrade3, hitUpgradeparents.position, hitUpgradeparents.rotation);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
