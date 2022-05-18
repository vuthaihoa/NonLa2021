using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFipped = false;
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;
        if(transform.position.x > player.position.x && isFipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFipped = false;
        }
        else if (transform.position.x < player.position.x && !isFipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFipped = true;
        }
    }
}
