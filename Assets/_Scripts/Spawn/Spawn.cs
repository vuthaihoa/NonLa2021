using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject platformUp;
    public GameObject platformUp1;
    public float NextSpawn;
    public float spawnTime;
    public float PointX1;
    public float pointX2;
    public float PointY1;
    public float pointY2;
    void Start()
    {
        
    }
    void Update()
    {
        if(Time.time >= spawnTime)
        {
            spawnTime = Time.time + NextSpawn;
            float x = Random.Range(PointX1, pointX2);
            float y = Random.Range(PointY1, pointY2);
            Vector3 spawn = new Vector3(x, y, 0);
            Instantiate(platformUp,spawn,Quaternion.identity);
            Instantiate(platformUp1, spawn, Quaternion.identity);
        }
    }

}
