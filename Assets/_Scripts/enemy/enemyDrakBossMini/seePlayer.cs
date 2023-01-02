using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seePlayer : MonoBehaviour
{
    private Transform player;
    public GameObject enemySaber;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool NonLa_hide = player.GetComponent<Animator>().GetBool("hide");
        if (collision.transform.tag == "Player")
        {
            if(!NonLa_hide)
            {
                enemySaber.SetActive(true);
            }
        }
    }
}
