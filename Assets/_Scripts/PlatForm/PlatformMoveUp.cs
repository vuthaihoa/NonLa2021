using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveUp : MonoBehaviour
{
    [SerializeField] private GameObject[] wapoints;
    private int currentWaypointsIndex = 0;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        if (Vector2.Distance(wapoints[currentWaypointsIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointsIndex++;
            if (currentWaypointsIndex >= wapoints.Length)
            {
                currentWaypointsIndex = 0;
                Destroy(transform.parent.gameObject);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wapoints[currentWaypointsIndex].transform.position, Time.deltaTime * speed);
    }
}
