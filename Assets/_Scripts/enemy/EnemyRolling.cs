using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyRolling : MonoBehaviour
{
    public float Speed;
    public float lineOfSite;
    public float startRoll;
    public float nextRoll;
    private Transform player;
    public GameObject damaColli;

    [SerializeField] private GameObject[] wapoints;
    private int currentWaypointsIndex = 0;


    Rigidbody2D Rg;
    Animator ani;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite )
        {
            Rolling();
        }
        else
        {
            ani.SetBool("attack", false);
        }
        Timer();
    }
    public void Rolling()
    {
        if (nextRoll < startRoll)
        {
            ani.SetBool("attack", true);
            startRoll -= Time.deltaTime;
            if (Vector2.Distance(wapoints[currentWaypointsIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointsIndex++;
                if (currentWaypointsIndex >= wapoints.Length)
                {
                    currentWaypointsIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, wapoints[currentWaypointsIndex].transform.position, Time.deltaTime * Speed);

        }
    }
    void Timer()
    {
        if (startRoll <= 1.5)
        {
            startRoll -= Time.deltaTime;
            ani.SetBool("attack", false);
            if (startRoll <= 0)
            {
                startRoll = 4;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
    public void BoxCollDashOn()
    {
        damaColli.SetActive(true);
    }
    public void BoxCollDashOff()
    {
        damaColli.SetActive(false);
    }
}
