using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public void lv1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
    public void lv2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }
    public void lv3()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }
    public void lv4()
    {
        SceneManager.LoadSceneAsync("Level 4");
    }
    public void lv5()
    {
        SceneManager.LoadSceneAsync("Level 5");
    }
}