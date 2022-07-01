using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private ManiMenu maniMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;
    private SaveSlot[] saveSlot;

    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlot = this.GetComponentsInChildren<SaveSlot>();
    }
    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
        if(!isLoadingGame)
        {
            DataPersistenceManager.instance.NewGame();
        }
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Level 1");
    }
    private void Start()
    {
        //ActivateMenu();
    }
    public void OnBackCliked()
    {
        maniMenu.ActivateMenu();
        this.DeactivateMenu();
    }
    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);
        this.isLoadingGame = isLoadingGame;
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();
        GameObject firstselected = backButton.gameObject;
        foreach(SaveSlot saveSlot in saveSlot)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);
            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if(firstselected.Equals(backButton.gameObject))
                {
                    firstselected = saveSlot.gameObject;
                }
            }
        }
        Button firstSelectedButton = firstselected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
    public void DisableMenuButtons()
    {
        foreach(SaveSlot saveSlot in saveSlot)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
