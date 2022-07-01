using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    //public AttributesScriptableObject playerAttributesData;

    public int maxHealth = 100;
    public int currentHealth;
    public int MoreHealth = 50;
    public int healthcolli = 1;

    public int money = 0;

    public int damage = 10;
    public int Magic = 15;

    public int HealthLv = 0;
    public int DamageLv = 0;
    public int MagicLv = 0;
    public int PotionLV = 0;

    public int soulFire = 0;

    public float UpgradeSoulHealth = 30;
    public float UpgradeSoulDamage = 30;
    public float UpgradeSoulMagic = 30;
    public float UpgradePotion = 50;

    public int buyPotion = 50;

    //public int NumberLevel = 0;
    public SerializableDictionary<string, bool> NumberLevel;
    public GameData()
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

        NumberLevel = new SerializableDictionary<string, bool>();
        //this.NumberLevel = 0;
        //playerPostion = Vector3.zero;
    }
    public int GetPercentageComplete()
    {
        int totalCollected = 0;
        foreach (bool collected in NumberLevel.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }
        int percentageCompleted = -1;
        if(NumberLevel.Count != 0)
        {
            percentageCompleted = (totalCollected * 100 / NumberLevel.Count);
        }
        return percentageCompleted;
    }
}
