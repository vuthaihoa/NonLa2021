using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseManeUI;
    public static PauseMenu instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauseManeUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    void Pause()
    {
        PauseManeUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    public void BackMenu()
    {
        PlayerPrefs.SetInt("LoadSaved", 1);
        SceneManager.LoadSceneAsync("Menu");
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Resume();
        DataPersistenceManager.instance.SaveGame();
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("LoadSaved", 1);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
        DataPersistenceManager.instance.SaveGame();
    }
}
