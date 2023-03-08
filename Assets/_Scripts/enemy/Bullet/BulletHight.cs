using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHight : MonoBehaviour
{
    [SerializeField] private float Hight;
    [SerializeField] private float low;
    [SerializeField] private float DistanceX;
    [SerializeField] private float DistanceX2;
    [SerializeField] protected float Timelife = 2f;
    [SerializeField] protected int Damage;
    [SerializeField] private float RorationTimne;
    Rigidbody2D Rb;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.AddForce(new Vector2(Random.Range(-DistanceX, DistanceX2),Random.Range(Hight,low)),ForceMode2D.Impulse);
        Destroy(gameObject, Timelife);
        RorationTimne = 0f;
        RorationTimne += Time.time;
        if (RorationTimne >= 0.6f)
        {
            this.transform.Rotate(0f, 0f, 180f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
