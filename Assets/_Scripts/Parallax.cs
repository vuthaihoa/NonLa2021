using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffexct;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffexct);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
