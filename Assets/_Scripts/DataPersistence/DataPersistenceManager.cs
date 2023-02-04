using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull =  false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSeclectedProfileId = "test";
    [Header("File Stonrage config")]
    [SerializeField]private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("Auto Save")]
    [SerializeField] private float autoSaveTimeSeconds = 1f;
    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObject;
    private FileDataHandler dataHandler;

    private string selectedProfileId = "";
    private Coroutine autoSaveCoruotine;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        if(disableDataPersistence)
        {
            Debug.LogWarning("Data persistence....");
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        
        InitializeSelectedProfileId();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObject = FindAllDataPersistionObject();
        LoadGame();
        if (autoSaveCoruotine != null)
        {
            StopCoroutine(autoSaveCoruotine);
        }
        autoSaveCoruotine = StartCoroutine(AutoSave());
    }
    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        LoadGame();
    }
    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
        InitializeSelectedProfileId();
        LoadGame();
    }
    private void InitializeSelectedProfileId()
    {
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSeclectedProfileId;
            Debug.LogWarning("overrode selected...." + testSeclectedProfileId);
        }
    }
    public void NewGame()
    {
        this.gameData = new GameData();
        playerAttributesSO.maxHealth = 100;
        playerAttributesSO.MoreHealth = 50;
        playerAttributesSO.healthcolli = 1;
        playerAttributesSO.money = 0;
        playerAttributesSO.damage = 10;
        playerAttributesSO.Magic = 15;
        playerAttributesSO.HealthLv = 0;
        playerAttributesSO.DamageLv = 0;
        playerAttributesSO.MagicLv = 0;
        playerAttributesSO.PotionLV = 0;
        playerAttributesSO.soulFire = 0;
        playerAttributesSO.UpgradeSoulHealth = 20;
        playerAttributesSO.UpgradeSoulDamage = 20;
        playerAttributesSO.UpgradeSoulMagic = 20;
        playerAttributesSO.UpgradePotion = 50;
        playerAttributesSO.buyPotion = 200;
        playerAttributesSO.damageHit = 1;
        playerAttributesSO.damageMagic = 100;
        playerAttributesSO.coolDown3 = 2;
        playerAttributesSO.UnlockBash = false;
        playerAttributesSO.UnlockBullet = false;
        playerAttributesSO.UnlockShield = false;
        playerAttributesSO.UnlockDash = false;
    }
    public void LoadGame()
    {
        if(disableDataPersistence)
        {
            return;
        }
        this.gameData = dataHandler.Load(selectedProfileId);
        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }
        if(this.gameData == null)
        {
            Debug.Log("no data");
            return;
        }
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObject)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        if(this.gameData == null)
        {
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObject)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        dataHandler.Save(gameData,selectedProfileId);
        gameData.lastUpdated = System.DateTime.Now.ToBinary();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistionObject()
    {
        IEnumerable<IDataPersistence> dataPersistenceObject = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObject);
    }
    public bool HasGameData()
    {
        return gameData != null;
    }
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
        }
    }
}
