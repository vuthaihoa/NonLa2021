using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int maxHealth = 100;
    public int currentHealth;
    public int damage =10;
    public int Magic = 15;

    public int HealthLv = 0;
    public int DamageLv = 0;
    public int MagicLv = 0;

    void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
        currentHealth = maxHealth;
    }
}
