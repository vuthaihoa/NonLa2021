using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMocTinh : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("MusicMocTinh");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        }
    }
}
