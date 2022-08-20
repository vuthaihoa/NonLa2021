using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_move : MonoBehaviour
{
    [SerializeField] private GameObject[] wapoints;
    private int currentWaypointsIndex = 0;
    [SerializeField] private float speed = 2f;
    Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(wapoints[currentWaypointsIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointsIndex++;
            if (currentWaypointsIndex >= wapoints.Length)
            {
                currentWaypointsIndex = 0;
            }
        }
        if(currentWaypointsIndex >= 2)
        {
            ani.SetBool("run", true);
            speed = 1f;
        }
        flip();
        transform.position = Vector2.MoveTowards(transform.position, wapoints[currentWaypointsIndex].transform.position, Time.deltaTime * speed);
    }
    void flip()
    {
        var scale = transform.localScale;
        if (currentWaypointsIndex == 1)
        {
            scale.x = -1f;
        }
        else
        {
            scale.x = 1;
        }
        transform.localScale = scale;
    }
}
