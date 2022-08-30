using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class earthquake : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, .2f, .2f);
        }
    }
}
