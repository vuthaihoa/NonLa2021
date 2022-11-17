using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

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
            CameraShaker.Instance.ShakeOnce(6f, 6f, .2f, .2f);
            FindObjectOfType<AudioManager>().Play("Golem");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Destroy(gameObject);
        }
    }
}
