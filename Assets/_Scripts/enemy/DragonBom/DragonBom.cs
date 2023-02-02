using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBom : MonoBehaviour
{
    [SerializeField] private GameObject[] wapoints;
    [SerializeField] private GameObject Stone;
    private int currentWaypointsIndex = 0;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        if (Vector2.Distance(wapoints[currentWaypointsIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointsIndex++;
            if(currentWaypointsIndex <= 1)
            {
                transform.Rotate(0f, 180f, 0);
            }
            if (currentWaypointsIndex >= wapoints.Length)
            {
                transform.Rotate(0f,180f,0);
                currentWaypointsIndex = 0;
                //Destroy(transform.parent.gameObject);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wapoints[currentWaypointsIndex].transform.position, Time.deltaTime * speed);
    }
    public void bom()
    {
        Instantiate(Stone,transform.position,Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
    }
}
