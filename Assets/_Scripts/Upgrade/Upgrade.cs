using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private int UpHealth = 20;
    [SerializeField]
    private Text healthText;

    [SerializeField]
    private int UpDamage = 10;
    [SerializeField]
    private Text DamageText;


    [SerializeField]
    private int UpMagic = 5;
    [SerializeField]
    private Text MagicText;

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
        stats.maxHealth = stats.maxHealth + UpHealth;
        stats.currentHealth = stats.maxHealth;
        UpdateHealth();
    }
    public void DamageUp()
    {
        stats.damage = stats.damage + UpDamage;
        UpdateDamage();
    }
    public void MagicUp()
    {
        stats.Magic = stats.Magic + UpMagic;
        UpdateMagic();
    }

}
