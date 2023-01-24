
using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "ScriptableObjects/AttributesScriptableObject", order = 1)]
public class AttributesScriptableObject : ScriptableObject
{
    public int maxHealth = 100;
    //public int currentHealth;
    public int MoreHealth = 50;
    public int healthcolli = 1;

    public int money = 0;
    
    public int damage = 10;
    public int damageHit = 40;
    public int Magic = 15;
    public int damageMagic = 100;

    public int HealthLv = 0;
    public int DamageLv = 0;
    public int MagicLv = 0;
    public int PotionLV = 0;

    public int soulFire = 0;

    public float UpgradeSoulHealth = 20;
    public float UpgradeSoulDamage = 20;
    public float UpgradeSoulMagic = 20;
    public float UpgradePotion = 50;

    public int buyPotion = 200;

    public int intLanguage = 0;
    [Header("UnLockSkill")]
    public bool UnlockDash =false;
    public bool UnlockShield = false;
    public bool UnlockBullet = false;
    public bool UnlockBash = false;
}