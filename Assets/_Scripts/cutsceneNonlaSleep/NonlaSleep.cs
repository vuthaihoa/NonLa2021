using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonlaSleep : MonoBehaviour
{
    public GameObject CutsceneNonlaSleep;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            CutsceneNonlaSleep.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            CutsceneNonlaSleep.SetActive(true);
        }
    }
}
