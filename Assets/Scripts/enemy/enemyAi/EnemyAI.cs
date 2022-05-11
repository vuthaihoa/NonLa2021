using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float MoveSpeed;
    public float attackDistance;
    public float timer;
    public Transform Left_limit;
    public Transform Right_limit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject HotZone;
    public GameObject triggerArea;

    private Animator ani;
    private float distance;
    private bool Attacking;
    private bool coooling;
    private float inttimer;

    void Awake()
    {
        SelectTarget();
        inttimer = timer;
        ani = GetComponent<Animator>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (!Attacking)
        {
            Move();
        }
        if (!Insideoflimits() && !inRange && !ani.GetCurrentAnimatorStateInfo(0).IsName("Wolf_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && coooling == false)
        {
            Attack();
        }
        if (coooling)
        {
            CoolDown();
            ani.SetBool("Attacking", false);
        }
    }
    void Move()
    {
        ani.SetBool("Canwalk", true);
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Wolf_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
        }
    }
    void Attack()
    {
        timer = inttimer;
        Attacking = true;
        ani.SetBool("Canwalk", false);
        ani.SetBool("Attacking", true);
        FindObjectOfType<AudioManager>().Play("wolfAttack");
    }
    void CoolDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && coooling && Attacking)
        {
            coooling = false;
            timer = inttimer;
        }
    }
    void StopAttack()
    {
        coooling = false;
        Attacking = false;
        ani.SetBool("Attacking", false);
    }
    public void TriggerCooling()
    {
        coooling = true;
    }
    private bool Insideoflimits()
    {
        return transform.position.x > Left_limit.position.x && transform.position.x < Right_limit.position.x;
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, Left_limit.position);
        float distanceToRight = Vector2.Distance(transform.position, Right_limit.position);
        if (distanceToLeft > distanceToRight)
        {
            target = Left_limit;
        }
        else
        {
            target = Right_limit;
        }
        flip();
    }
    public void flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }
        transform.eulerAngles = rotation;
    }
}

