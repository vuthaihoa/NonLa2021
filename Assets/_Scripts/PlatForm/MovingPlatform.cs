using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] wapoints;
    private int currentWaypointsIndex = 0;
    [SerializeField] private float speed = 2f;

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
        flip();
        transform.position = Vector2.MoveTowards(transform.position, wapoints[currentWaypointsIndex].transform.position, Time.deltaTime * speed);
    }
    void flip()
    {
        var scale = transform.localScale;
        if (currentWaypointsIndex == 1)
        {
            scale.x = -2.235468f;
        }
        else
        {
            scale.x = 2.235468f;
        }
        transform.localScale = scale;
    }
}
