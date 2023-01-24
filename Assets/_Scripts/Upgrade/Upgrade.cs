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
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    void OnEnable()
    {
        stats = PlayerStats.instance;
        playerAttributesSO.HealthLv += 0;
        playerAttributesSO.DamageLv += 0;
        playerAttributesSO.MagicLv += 0;
    }
    private void Update()
    {
        healthText.text = playerAttributesSO.HealthLv.ToString();
        DamageText.text = playerAttributesSO.DamageLv.ToString();
        MagicText.text = playerAttributesSO.MagicLv.ToString();

        healthTextUp.text = playerAttributesSO.UpgradeSoulHealth.ToString();
        DamageTextUp.text = playerAttributesSO.UpgradeSoulDamage.ToString();
        MagicTextUp.text = playerAttributesSO.UpgradeSoulMagic.ToString();

        potionLV_Text.text = playerAttributesSO.PotionLV.ToString();
        potionUpgrade.text = playerAttributesSO.UpgradePotion.ToString();
        BuyPotionText.text = playerAttributesSO.buyPotion.ToString();

        healthTextUp.text = Mathf.Round(playerAttributesSO.UpgradeSoulHealth).ToString();
        DamageTextUp.text = Mathf.Round(playerAttributesSO.UpgradeSoulDamage).ToString();
        MagicTextUp.text = Mathf.Round(playerAttributesSO.UpgradeSoulMagic).ToString();
        potionUpgrade.text = Mathf.Round(playerAttributesSO.UpgradePotion).ToString();
    }
    void UpdateHealth()
    {
        playerAttributesSO.HealthLv += 1;
        healthText.text = playerAttributesSO.HealthLv.ToString();
    }
    void UpdateDamage()
    {
        playerAttributesSO.DamageLv += 1;
        DamageText.text = playerAttributesSO.DamageLv.ToString();
    }
    void UpdateMagic()
    {
        playerAttributesSO.MagicLv += 1;
        MagicText.text = playerAttributesSO.MagicLv.ToString();
    }
    public void HealthUp()
    {
        if(playerAttributesSO.soulFire < playerAttributesSO.UpgradeSoulHealth)
        {
            return;
        }
        playerAttributesSO.maxHealth = playerAttributesSO.maxHealth + UpHealth;
        stats.currentHealth = playerAttributesSO.maxHealth;
        playerAttributesSO.soulFire = (int)(playerAttributesSO.soulFire - playerAttributesSO.UpgradeSoulHealth);
        playerAttributesSO.UpgradeSoulHealth *= 1.3f;
        healthTextUp.text = Mathf.Round(playerAttributesSO.UpgradeSoulHealth).ToString();
        UpdateHealth();
    }
    public void DamageUp()
    {
        if (playerAttributesSO.soulFire < playerAttributesSO.UpgradeSoulDamage)
        {
            return;
        }
        playerAttributesSO.damage = playerAttributesSO.damage + UpDamage;
        UpdateDamage();
        playerAttributesSO.soulFire = (int)(playerAttributesSO.soulFire - playerAttributesSO.UpgradeSoulDamage);
        playerAttributesSO.UpgradeSoulDamage *= 1.3f;
        DamageTextUp.text = Mathf.Round(playerAttributesSO.UpgradeSoulDamage).ToString();
    }
    public void MagicUp()
    {
        if (playerAttributesSO.soulFire < playerAttributesSO.UpgradeSoulMagic)
        {
            return;
        }
        playerAttributesSO.Magic = playerAttributesSO.Magic + UpMagic;
        UpdateMagic();
        playerAttributesSO.soulFire = (int)(playerAttributesSO.soulFire - playerAttributesSO.UpgradeSoulMagic);
        playerAttributesSO.UpgradeSoulMagic *= 1.3f;
        MagicTextUp.text = Mathf.Round(playerAttributesSO.UpgradeSoulMagic).ToString();
    }
    void moneyText()
    {
        playerAttributesSO.PotionLV += 1;
        potionLV_Text.text = playerAttributesSO.PotionLV.ToString();
    }
    public void PotionUp()
    {
        if(playerAttributesSO.money < playerAttributesSO.UpgradePotion)
        {
            return;
        }
        playerAttributesSO.MoreHealth = playerAttributesSO.MoreHealth + UpPotion;
        moneyText();
        playerAttributesSO.money = (int)(playerAttributesSO.money - playerAttributesSO.UpgradePotion);
        playerAttributesSO.UpgradePotion *=1.3f;
        potionUpgrade.text = Mathf.Round(playerAttributesSO.UpgradePotion).ToString();

    }
    public void BuyPotion()
    {
        if(playerAttributesSO.money < playerAttributesSO.buyPotion)
        {
            return;
        }
        playerAttributesSO.healthcolli = playerAttributesSO.healthcolli + 1;
        playerAttributesSO.money = playerAttributesSO.money - playerAttributesSO.buyPotion;
        BuyPotionText.text = playerAttributesSO.buyPotion.ToString();

    }

}
