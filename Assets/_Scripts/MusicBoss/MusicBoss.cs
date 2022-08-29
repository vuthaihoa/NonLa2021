using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("DanhBoss");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
    }
}
