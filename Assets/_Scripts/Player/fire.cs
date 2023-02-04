using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public GameObject magic1;
    public GameObject magic2;
    Animator ani;
    private bool die = true;
    PlayerController player;
    public Image CoolDownBullet;
    private bool IsCoolDown3 = false;
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        CoolDownBullet.fillAmount = 0;
    }
    void Update()
    {
        if(die)
        {
            Fire();
        }
        if(PlayerStats.instance.currentHealth <= 0)
        {
            die = false;
        }
        if(playerAttributesSO.UnlockBullet == false)
        {
            CoolDownBullet.fillAmount = 1f;
        }
    }
    public void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("PlayerBullet");
    }
    public void Noshoot()
    {
        ani.SetBool("Bullet", false);
    }
    public void Magic1()
    {
        if(playerAttributesSO.Magic >= 50)
        {
            Instantiate(magic1, firePoint.position, firePoint.rotation);
        }
    }
    public void Magic2()
    {
        if (playerAttributesSO.Magic >= 70)
        {
            Instantiate(magic2, firePoint.position, firePoint.rotation);
        }
    }
    void Fire()
    {
        if(playerAttributesSO.UnlockBullet == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && IsCoolDown3 == false)
            {
                IsCoolDown3 = true;
                CoolDownBullet.fillAmount = 1f;
                ani.SetBool("Bullet", true);
                ani.SetBool("hide", false);
                player.hide = IsCoolDown3;

            }
            if (IsCoolDown3)
            {
                CoolDownBullet.fillAmount -= 1 / playerAttributesSO.coolDown3 * Time.deltaTime;
                if (CoolDownBullet.fillAmount <= 0)
                {
                    CoolDownBullet.fillAmount = 0;
                    IsCoolDown3 = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "UnlockBullet")
        {
            playerAttributesSO.UnlockBullet = true;
            CoolDownBullet.fillAmount = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UnlockBullet")
        {
            playerAttributesSO.UnlockBullet = true;
            CoolDownBullet.fillAmount = 0;
        }
    }
}
