using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private GameObject[] wapoints;
    private int currentWaypointsIndex = 0;
    [SerializeField] private float speed = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            movePoint();
        }
        transform.position = Vector2.MoveTowards(transform.position, wapoints[currentWaypointsIndex].transform.position, Time.deltaTime * speed);
    }
    private void movePoint()
    {
        if (currentWaypointsIndex == 3)
        {
            speed = 0.8f;
        }
        if (currentWaypointsIndex == 4)
        {
            speed = 1.1f;
        }
        if (currentWaypointsIndex == 5)
        {
            speed = 1.3f;
        }
        if (currentWaypointsIndex == 6)
        {
            speed = 0.6f;
        }
        if (currentWaypointsIndex == 7)
        {
            Roration(player);
        }
        if (currentWaypointsIndex == 8)
        {
            Roration(player);
        }
        if (currentWaypointsIndex == 9)
        {
            speed = 1.3f;
        }
        if (currentWaypointsIndex == 10)
        {
            Roration(player);
            speed = 0.6f;
        }
        if (currentWaypointsIndex == 13)
        {
            Destroy(gameObject);
        }
    }
    private void Roration(Transform target)
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200);
    }
}
