using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManiMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    //public void PlayGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
    private void Start()
    {
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }
    public void OnLoadGameClicked() 
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();

    }
    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        if (PlayerPrefs.GetInt("LoadSaved") == 1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        }
        else
        {
            return;
        }
        //SceneManager.LoadSceneAsync("SampleScene");
    }
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
