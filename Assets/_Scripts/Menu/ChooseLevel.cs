using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public void lv1()
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadSceneAsync("Level 1");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
    }
    public void lv2()
    {
        SceneManager.LoadSceneAsync("Level 2");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        playerStorage.initialValue = playerPosition;
    }
    public void lv3()
    {
        SceneManager.LoadSceneAsync("Level 3");
        FindObjectOfType<AudioManager>().Play("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        playerStorage.initialValue = playerPosition;
    }
    public void lv4()
    {
        SceneManager.LoadSceneAsync("Level 4");
        FindObjectOfType<AudioManager>().Play("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        playerStorage.initialValue = playerPosition;
    }
    public void lv5()
    {
        SceneManager.LoadSceneAsync("Level 5");
        FindObjectOfType<AudioManager>().Play("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        playerStorage.initialValue = playerPosition;
    }
    public void lv6()
    {
        SceneManager.LoadSceneAsync("Level 6");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        playerStorage.initialValue = playerPosition;
    }
    public void lv7()
    {
        SceneManager.LoadSceneAsync("Level 7");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Bodyguard");
        playerStorage.initialValue = playerPosition;
    }
    public void lv8()
    {
        SceneManager.LoadSceneAsync("Level 8");
        FindObjectOfType<AudioManager>().Play("Bodyguard");
        FindObjectOfType<AudioManager>().StopPlaying("DanhBoss");
        FindObjectOfType<AudioManager>().StopPlaying("MusicMocTinh");
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        playerStorage.initialValue = playerPosition;
    }
}
