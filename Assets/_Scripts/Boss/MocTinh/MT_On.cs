using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MT_On : MonoBehaviour
{
    public GameObject Boss;
    public GameObject stone;
    public GameObject stone2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            Boss.SetActive(true);
            stone.SetActive(true);
            stone2.SetActive(true);
        }
    }
}
