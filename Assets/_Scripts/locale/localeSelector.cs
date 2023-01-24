using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class localeSelector : MonoBehaviour
{
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    private void Start()
    {
        ChangeLocale(playerAttributesSO.intLanguage);
    }
    private void Update()
    {
        ChangeLocale(playerAttributesSO.intLanguage);
    }
    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        playerAttributesSO.intLanguage = localeID;
        if (active == true)
            return;
        StartCoroutine(SetLoacle(localeID));
    }
    IEnumerator SetLoacle(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;

    }
}
