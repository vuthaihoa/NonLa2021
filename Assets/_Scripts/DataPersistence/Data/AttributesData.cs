using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesData
{
    public int maxHealth;
    public int currentHealth;
    public int MoreHealth;
    public int healthcolli;

    public int money;

    public int damage;
    public int Magic;

    public int HealthLv;
    public int DamageLv;
    public int MagicLv;
    public int PotionLV;

    public int soulFire;

    public float UpgradeSoulHealth;
    public float UpgradeSoulDamage;
    public float UpgradeSoulMagic;
    public float UpgradePotion;

    public int buyPotion;
    public AttributesData()
    {
        this.maxHealth = 100;
        this.currentHealth = 100;
        this.MoreHealth = 50;
        this.healthcolli = 1;
        this.money = 0;
        this.damage = 10;
        this.Magic = 15;
        this.HealthLv = 0;
        this.DamageLv = 0;
        this.MagicLv = 0;
        this.PotionLV = 0;
        this.soulFire = 0;
        this.UpgradeSoulHealth = 30;
        this.UpgradeSoulDamage = 30;
        this.UpgradeSoulMagic = 30;
        this.UpgradePotion = 50;
        this.buyPotion = 50;
    }
}
