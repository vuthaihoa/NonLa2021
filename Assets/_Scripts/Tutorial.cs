using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
        {
            ani.SetBool("ok", false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetBool("ok", true);
        }
    }
}
