using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private int UpHealth = 20;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text healthTextUp;
 


    [SerializeField]
    private int UpDamage = 10;
    [SerializeField]
    private Text DamageText;
    [SerializeField]
    private Text DamageTextUp;



    [SerializeField]
    private int UpMagic = 5;
    [SerializeField]
    private Text MagicText;
    [SerializeField]
    private Text MagicTextUp;



    private PlayerStats stats;
    void OnEnable()
    {
        stats = PlayerStats.instance;
        stats.HealthLv += 0;
        stats.DamageLv += 0;
        stats.MagicLv += 0;

        healthText.text = stats.HealthLv.ToString();
        DamageText.text = stats.DamageLv.ToString();
        MagicText.text = stats.MagicLv.ToString();

        healthTextUp.text = stats.UpgradeSoulHealth.ToString();
        DamageTextUp.text = stats.UpgradeSoulDamage.ToString();
        MagicTextUp.text = stats.UpgradeSoulMagic.ToString();
    }
    void UpdateHealth()
    {
        stats.HealthLv += 1;
        healthText.text = stats.HealthLv.ToString();
    }
    void UpdateDamage()
    {
        stats.DamageLv += 1;
        DamageText.text = stats.DamageLv.ToString();
    }
    void UpdateMagic()
    {
        stats.MagicLv += 1;
        MagicText.text = stats.MagicLv.ToString();
    }
    public void HealthUp()
    {
        if(stats.soulFire < stats.UpgradeSoulHealth)
        {
            return;
        }
        stats.maxHealth = stats.maxHealth + UpHealth;
        stats.currentHealth = stats.maxHealth;
        stats.soulFire = (int)(stats.soulFire - stats.UpgradeSoulHealth);
        stats.UpgradeSoulHealth *= 1.3f;
        healthTextUp.text = stats.UpgradeSoulHealth.ToString();
        UpdateHealth();
    }
    public void DamageUp()
    {
        if (stats.soulFire < stats.UpgradeSoulDamage)
        {
            return;
        }
        stats.damage = stats.damage + UpDamage;
        UpdateDamage();
        stats.soulFire = (int)(stats.soulFire - stats.UpgradeSoulDamage);
        stats.UpgradeSoulDamage *= 1.3f;
        DamageTextUp.text = stats.UpgradeSoulDamage.ToString();
    }
    public void MagicUp()
    {
        if (stats.soulFire < stats.UpgradeSoulMagic)
        {
            return;
        }
        stats.Magic = stats.Magic + UpMagic;
        UpdateMagic();
        stats.soulFire = (int)(stats.soulFire - stats.UpgradeSoulMagic);
        stats.UpgradeSoulMagic *= 1.3f;
        MagicTextUp.text = stats.UpgradeSoulMagic.ToString();
    }

}
