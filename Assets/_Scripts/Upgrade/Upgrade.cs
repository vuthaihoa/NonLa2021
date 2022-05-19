using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private int UpHealth = 20;
    [SerializeField]
    private Text healthText;
    int HealthLv = 0;

    [SerializeField]
    private int UpDamage = 10;
    [SerializeField]
    private Text DamageText;
    int DamageLv = 0;

    [SerializeField]
    private int UpMagic = 5;
    [SerializeField]
    private Text MagicText;
    int MagicLv = 0;
    private PlayerStats stats;
    void OnEnable()
    {
        stats = PlayerStats.instance;   
    }
    void UpdateHealth()
    {
        HealthLv += 1;
        healthText.text = HealthLv.ToString();
    }
    void UpdateDamage()
    {
        DamageLv += 1;
        DamageText.text = DamageLv.ToString();
    }
    void UpdateMagic()
    {
        MagicLv += 1;
        MagicText.text = MagicLv.ToString();
    }
    public void HealthUp()
    {
        stats.maxHealth = stats.maxHealth + UpHealth;
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
