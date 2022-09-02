using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBossBodyguard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Bodyguard");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
        }
    }
}
