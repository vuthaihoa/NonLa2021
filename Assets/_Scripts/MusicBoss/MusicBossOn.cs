using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBossOn : MonoBehaviour
{
    public GameObject Musicboss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Musicboss.SetActive(true);
        }
    }
}
