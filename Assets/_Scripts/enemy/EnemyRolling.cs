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
    bool faceingRight = false;

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
        if (startRoll <= 3)
        {
            startRoll -= Time.deltaTime;
            ani.SetBool("attack", false);
            if (startRoll <= 0)
            {
                startRoll = 6;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
    public void flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > player.position.x)
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
