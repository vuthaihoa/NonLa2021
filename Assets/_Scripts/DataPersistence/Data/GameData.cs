using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int maxHealth = 100;
    public int currentHealth;
    public int MoreHealth = 30;
    public int healthcolli = 0;

    public int money;

    public int damage = 10;
    public int Magic = 15;

    public int HealthLv = 0;
    public int DamageLv = 0;
    public int MagicLv = 0;
    public int PotionLV = 0;

    public int soulFire = 0;

    public float UpgradeSoulHealth = 0;
    public float UpgradeSoulDamage = 0;
    public float UpgradeSoulMagic = 0;
    public float UpgradePotion = 0;

    public int buyPotion;

    public Vector3 playerPostion;
    public GameData()
    {
        this.maxHealth = 100;
        this.currentHealth = 100;
        this.MoreHealth = 0;
        this.healthcolli = 0;
        this.money = 0;
        this.damage = 10;
        this.Magic = 15;
        this.HealthLv = 0;
        this.DamageLv = 0;
        this.MagicLv = 0;
        this.PotionLV = 0;
        this.soulFire = 0;
        this.UpgradeSoulHealth = 0;
        this.UpgradeSoulDamage = 0;
        this.UpgradeSoulMagic = 0;
        this.UpgradePotion = 0;
        this.buyPotion = 0;
        playerPostion = Vector3.zero;
    }
}
