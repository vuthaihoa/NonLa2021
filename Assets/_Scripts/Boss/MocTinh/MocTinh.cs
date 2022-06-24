using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocTinh : MonoBehaviour
{
    public GameObject bulletEnemyRight;
    public Transform bulletParentRgiht;
    public Transform bulletParentLeft;
    public GameObject bulletEnemyLeft;
    public void Bullet()
    {
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(bulletEnemyRight, bulletParentRgiht.transform.position, Quaternion.identity);
        Instantiate(bulletEnemyLeft, bulletParentLeft.transform.position, Quaternion.identity);
    }
}
