using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static PlayerStats instance;
    //public int maxHealth = 100;
    public int currentHealth;
    //public int MoreHealth = 30;
    //public int healthcolli;

    //public int money;

    //public int damage = 10;
    //public int Magic = 15;

    //public int HealthLv = 0;
    //public int DamageLv = 0;
    //public int MagicLv = 0;
    //public int PotionLV = 0;

    //public int soulFire;

    //public float UpgradeSoulHealth;
    //public float UpgradeSoulDamage;
    //public float UpgradeSoulMagic;
    //public float UpgradePotion;

    //public int buyPotion;

    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        currentHealth = playerAttributesSO.maxHealth;
        DontDestroyOnLoad(this.gameObject);
        
    }
    public void LoadData(GameData data)
    {
        playerAttributesSO.maxHealth = data.playerAttributesData.maxHealth;
        //playerAttributesSO.currentHealth = data.playerAttributesData.currentHealth;
        playerAttributesSO.MoreHealth = data.playerAttributesData.MoreHealth;
        playerAttributesSO.healthcolli = data.playerAttributesData.healthcolli;
        playerAttributesSO.money = data.playerAttributesData.money;
        playerAttributesSO.damage = data.playerAttributesData.damage;
        playerAttributesSO.Magic = data.playerAttributesData.Magic;
        playerAttributesSO.HealthLv = data.playerAttributesData.HealthLv;
        playerAttributesSO.DamageLv = data.playerAttributesData.DamageLv;
        playerAttributesSO.MagicLv = data.playerAttributesData.MagicLv;
        playerAttributesSO.PotionLV = data.playerAttributesData.PotionLV;
        playerAttributesSO.soulFire = data.playerAttributesData.soulFire;
        playerAttributesSO.UpgradeSoulHealth = data.playerAttributesData.UpgradeSoulHealth;
        playerAttributesSO.UpgradeSoulDamage = data.playerAttributesData.UpgradeSoulDamage;
        playerAttributesSO.UpgradeSoulMagic = data.playerAttributesData.UpgradeSoulMagic;
        playerAttributesSO.UpgradePotion = data.playerAttributesData.UpgradePotion;
        playerAttributesSO.buyPotion = data.playerAttributesData.buyPotion;
        playerAttributesSO.intLanguage = data.playerAttributesData.intLanguage;
    }
    public void SaveData(GameData data)
    {
        data.playerAttributesData.maxHealth = playerAttributesSO.maxHealth;
        //data.playerAttributesData.currentHealth = playerAttributesSO.currentHealth;
        data.playerAttributesData.MoreHealth = playerAttributesSO.MoreHealth;
        data.playerAttributesData.healthcolli = playerAttributesSO.healthcolli;
        data.playerAttributesData.money = playerAttributesSO.money;
        data.playerAttributesData.damage = playerAttributesSO.damage;
        data.playerAttributesData.Magic = playerAttributesSO.Magic ;
        data.playerAttributesData.HealthLv = playerAttributesSO.HealthLv;
        data.playerAttributesData.DamageLv = playerAttributesSO.DamageLv;
        data.playerAttributesData.MagicLv = playerAttributesSO.MagicLv;
        data.playerAttributesData.PotionLV = playerAttributesSO.PotionLV;
        data.playerAttributesData.soulFire = playerAttributesSO.soulFire ;
        data.playerAttributesData.UpgradeSoulHealth = playerAttributesSO.UpgradeSoulHealth;
        data.playerAttributesData.UpgradeSoulDamage = playerAttributesSO.UpgradeSoulDamage;
        data.playerAttributesData.UpgradeSoulMagic = playerAttributesSO.UpgradeSoulMagic;
        data.playerAttributesData.UpgradePotion = playerAttributesSO.UpgradePotion;
        data.playerAttributesData.buyPotion = playerAttributesSO.buyPotion;
        data.playerAttributesData.intLanguage = playerAttributesSO.intLanguage;
    }
}
