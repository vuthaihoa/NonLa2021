using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneNewGame : MonoBehaviour
{
    public float ChangeTime;
    public string sceneName;
    void Update()
    {
        ChangeTime -= Time.deltaTime;
        if(ChangeTime <=0)
        {
            SceneManager.LoadScene(sceneName);
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeTime = 0;
        }
    }
}
