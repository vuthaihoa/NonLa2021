using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class localeSelector : MonoBehaviour
{
    //private static localeSelector instance;
    //private void Start()
    //{
    //    int ID = PlayerPrefs.GetInt("LocaleKey", 0);
    //    ChangeLocale(ID);
    //}
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(instance);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //}
    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if(active ==  true)
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
