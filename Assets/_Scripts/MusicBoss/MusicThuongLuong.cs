using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicThuongLuong : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("DanhBoss");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            Destroy(gameObject, 2f);
        }
    }
}
