using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject Upgrade;
    public void ActivateUpgrade()
    {
        Upgrade.SetActive(true);
    }
    public bool UpgradeActivete()
    {
        return Upgrade.activeInHierarchy;
    }
}
