using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameMaster gm;
    public VectorValue vectorValue;
    Animator ani;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        ani = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gm.lastCheckPoint = transform.position;
            ani.SetBool("CheckPoint", true);
            vectorValue.initialValue = gm.lastCheckPoint;
            //FindObjectOfType<AudioManager>().Play("checkPoint");
        }
    }
}
