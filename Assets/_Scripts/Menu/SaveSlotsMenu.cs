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
    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu;
    private SaveSlot[] saveSlot;

    private bool isLoadingGame = false;


    private void Awake()
    {
        saveSlot = this.GetComponentsInChildren<SaveSlot>();
    }
    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();
        if (isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
            SaveGameAndLoadScene();
            //DataPersistenceManager.instance.NewGame();
            //SceneManager.LoadSceneAsync("NewGame");
        }
        else if(saveSlot.hasData)
        {
            confirmationPopupMenu.ActivateMenu(
                "Starting a New Game With this Slot will override the  currently saved data. Are you sure?",
                () =>
                {
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                    SceneManager.LoadSceneAsync("NewGame");
                },
                () =>
                {
                    this.ActivateMenu(isLoadingGame);
                }
                );
        }
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
            SceneManager.LoadSceneAsync("NewGame");
        }
    }
    private void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();
        if (PlayerPrefs.GetInt("LoadSaved") == 1 && isLoadingGame)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        }
        else
        {
            return;
        }
    }
    public void OnClearClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();
        confirmationPopupMenu.ActivateMenu(
            "Are you sure you want to delete this Save data?",
            () =>
            {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
                ActivateMenu(isLoadingGame);
            },
            () =>
            {
                ActivateMenu(isLoadingGame);
            }
            );

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
        backButton.interactable = true;
        backButton.interactable = true;
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
        //StartCoroutine(this.SetFirstSelected(firstselected));
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
