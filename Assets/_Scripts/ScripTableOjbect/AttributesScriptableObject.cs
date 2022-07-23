
using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "ScriptableObjects/AttributesScriptableObject", order = 1)]
public class AttributesScriptableObject : ScriptableObject
{
    public int maxHealth ;
    public int currentHealth;
    public int MoreHealth ;
    public int healthcolli;

    public int money;
    
    public int damage;
    public int Magic;

    public int HealthLv;
    public int DamageLv;
    public int MagicLv;
    public int PotionLV ;

    public int soulFire;

    public float UpgradeSoulHealth;
    public float UpgradeSoulDamage;
    public float UpgradeSoulMagic;
    public float UpgradePotion;

    public int buyPotion;
}