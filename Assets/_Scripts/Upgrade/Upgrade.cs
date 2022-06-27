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
    private int UpDamage = 5 ;
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

    [SerializeField]
    private int UpPotion = 20;
    [SerializeField]
    private Text potionLV_Text;
    [SerializeField]
    private Text potionUpgrade;
    [SerializeField]
    private Text BuyPotionText;

    private PlayerStats stats;
    void OnEnable()
    {
        stats = PlayerStats.instance;
        stats.HealthLv += 0;
        stats.DamageLv += 0;
        stats.MagicLv += 0;
    }
    private void Update()
    {
        healthText.text = stats.HealthLv.ToString();
        DamageText.text = stats.DamageLv.ToString();
        MagicText.text = stats.MagicLv.ToString();

        healthTextUp.text = stats.UpgradeSoulHealth.ToString();
        DamageTextUp.text = stats.UpgradeSoulDamage.ToString();
        MagicTextUp.text = stats.UpgradeSoulMagic.ToString();

        potionLV_Text.text = stats.PotionLV.ToString();
        potionUpgrade.text = stats.UpgradePotion.ToString();
        BuyPotionText.text = stats.buyPotion.ToString();

        healthTextUp.text = Mathf.Round(stats.UpgradeSoulHealth).ToString();
        DamageTextUp.text = Mathf.Round(stats.UpgradeSoulDamage).ToString();
        MagicTextUp.text = Mathf.Round(stats.UpgradeSoulMagic).ToString();
        potionUpgrade.text = Mathf.Round(stats.UpgradePotion).ToString();
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
        healthTextUp.text = Mathf.Round(stats.UpgradeSoulHealth).ToString();
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
        DamageTextUp.text = Mathf.Round(stats.UpgradeSoulDamage).ToString();
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
        MagicTextUp.text = Mathf.Round(stats.UpgradeSoulMagic).ToString();
    }
    void moneyText()
    {
        stats.PotionLV += 1;
        potionLV_Text.text = stats.PotionLV.ToString();
    }
    public void PotionUp()
    {
        if(stats.money < stats.UpgradePotion)
        {
            return;
        }
        stats.MoreHealth = stats.MoreHealth + UpPotion;
        moneyText();
        stats.money = (int)(stats.money - stats.UpgradePotion);
        stats.UpgradePotion *=1.3f;
        potionUpgrade.text = Mathf.Round(stats.UpgradePotion).ToString();

    }
    public void BuyPotion()
    {
        if(stats.money < stats.buyPotion)
        {
            return;
        }
        stats.healthcolli = stats.healthcolli + 1;
        stats.money = stats.money - stats.buyPotion;
        stats.buyPotion = stats.buyPotion + 20;
        BuyPotionText.text = stats.buyPotion.ToString();

    }

}
