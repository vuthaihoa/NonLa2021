using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    Animator ani;
    public AudioSource audio_S;
    public AudioClip[] sonido;

    public int combo;
    public bool atacando;
    public bool die = true;

    float NextAttackTime = 0f;
    public float StartTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatEnemies;
    public float attackRange;
    public GameObject hitParticle;
    void Start()
    {
        ani = GetComponent<Animator>();
        audio_S = GetComponent<AudioSource>();
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !atacando)
        {
            atacando = true;
            ani.SetTrigger("" + combo);
            audio_S.clip = sonido[combo];
            audio_S.Play();
        }
    }
    void Update()
    {
        if(die)
        {
            if (Time.time >= NextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Combo_();
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy_Health>().Takedamage(PlayerStats.instance.damage);
                        Instantiate(hitParticle, attackPos.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                        NextAttackTime = Time.time + 1f / StartTimeBtwAttack;
                    }
                }
            }
        }
        if(PlayerStats.instance.currentHealth <= 0)
        {
            die = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
