using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneNewGame : MonoBehaviour
{
    public float ChangeTime;
    public string sceneName;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    void Update()
    {
        ChangeTime -= Time.deltaTime;
        if(ChangeTime <=0)
        {
            SceneManager.LoadScene(sceneName);
            playerStorage.initialValue = playerPosition;
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeTime = 0;
            playerStorage.initialValue = playerPosition;
        }
    }
}
