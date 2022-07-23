using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour,IDataPersistence
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public int NumberLevel = 1;
    public static LevelLoader instance;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            NumberLevel +=1;
            DataPersistenceManager.instance.SaveGame();

        }
    }
    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data.NumberLevel)
        {
            if (pair.Value)
            {
                NumberLevel++;
            }
        }
    }
    public void SaveData(GameData data)
    {

    }
}
