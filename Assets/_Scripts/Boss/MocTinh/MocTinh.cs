using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocTinh : MonoBehaviour
{
    public GameObject bulletEnemyRight;
    public GameObject bulletParentLeft;
    public GameObject bulletParentRgiht;
    public GameObject bulletEnemyLeft;
    public void Bullet()
    {
        FindObjectOfType<AudioManager>().Play("EnemyBullet");
        Instantiate(bulletEnemyRight, bulletParentRgiht.transform.position, Quaternion.identity);
        Instantiate(bulletEnemyLeft, bulletParentLeft.transform.position, Quaternion.identity);
    }
}
