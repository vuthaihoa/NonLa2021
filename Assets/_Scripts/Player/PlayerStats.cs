using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static PlayerStats instance;
    public int maxHealth = 100;
    public int currentHealth;
    public int MoreHealth = 30;
    public int healthcolli;

    public int money;

    public int damage =10;
    public int Magic = 15;

    public int HealthLv = 0;
    public int DamageLv = 0;
    public int MagicLv = 0;
    public int PotionLV = 0;

    public int soulFire;

    public float UpgradeSoulHealth;
    public float UpgradeSoulDamage;
    public float UpgradeSoulMagic;
    public float UpgradePotion;

    public int buyPotion;
    void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
        currentHealth = maxHealth;
    }
    public void LoadData(GameData data)
    {
        this.maxHealth = data.maxHealth;
        this.currentHealth = data.maxHealth;
        this.MoreHealth = data.MoreHealth;
        this.healthcolli = data.healthcolli;
        this.money = data.money;
        this.damage = data.damage;
        this.Magic = data.Magic;
        this.HealthLv = data.HealthLv;
        this.DamageLv = data.DamageLv;
        this.MagicLv = data.MagicLv;
        this.PotionLV = data.PotionLV;
        this.soulFire = data.soulFire;
        this.UpgradeSoulHealth = data.UpgradeSoulHealth;
        this.UpgradeSoulDamage = data.UpgradeSoulDamage;
        this.UpgradeSoulMagic = data.UpgradeSoulMagic;
        this.UpgradePotion = data.UpgradePotion;
        this.buyPotion = data.buyPotion;
    }
    public void SaveData(ref GameData data)
    {
        data.maxHealth = this.maxHealth;
        data.maxHealth = this.currentHealth;
        data.MoreHealth = this.MoreHealth;
        data.healthcolli = this.healthcolli;
        data.money = this.money;
        data.damage = this.damage;
        data.Magic = this.Magic;
        data.HealthLv = this.HealthLv;
        data.DamageLv = this.DamageLv;
        data.MagicLv = this.MagicLv;
        data.PotionLV = this.PotionLV;
        data.soulFire = this.soulFire;
        data.UpgradeSoulHealth = this.UpgradeSoulHealth;
        data.UpgradeSoulDamage = this.UpgradeSoulDamage;
        data.UpgradeSoulMagic = this.UpgradeSoulMagic;
        data.UpgradePotion = this.UpgradePotion;
        data.buyPotion = this.buyPotion;
    }
}
