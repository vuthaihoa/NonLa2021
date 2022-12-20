using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBoss : MonoBehaviour
{
    public GameObject Nameboss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Nameboss.SetActive(true);
            Destroy(transform.parent.gameObject, 3f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Nameboss.SetActive(true);
            Destroy(transform.parent.gameObject, 3f);
        }
    }
}
