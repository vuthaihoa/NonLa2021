using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormPrivate : MonoBehaviour
{
    public GameObject privatePlatform;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            privatePlatform.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            privatePlatform.SetActive(true);
        }
    }
}
