using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkill : MonoBehaviour
{
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void Done()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ani.SetTrigger("Unlock");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetTrigger("Unlock");
        }
    }
}
