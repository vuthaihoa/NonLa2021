using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour,IDataPersistence
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public static LevelLoader instance;
    private int NumberLevel = 0;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
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
    private void Start()
    {
        // subscribe to events
        GameEventsManager.instance.onCoinCollected += OnCoinCollected;
    }
    private void OnDestroy()
    {
        // unsubscribe from events
        GameEventsManager.instance.onCoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        NumberLevel++;
    }
}
