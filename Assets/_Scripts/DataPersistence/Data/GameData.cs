using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    //public AttributesScriptableObject playerAttributesData;

    public AttributesData playerAttributesData;
    //public int NumberLevel = 0;
    public SerializableDictionary<string, bool> NumberLevel;
    public GameData()
    {

        playerAttributesData = new AttributesData();
        NumberLevel = new SerializableDictionary<string, bool>();
        //this.NumberLevel = 0;
        //playerPostion = Vector3.zero;
    }
    public int GetPercentageComplete()
    {
        int totalCollected = 1;
        foreach (bool collected in NumberLevel.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }
        int percentageCompleted = 1;
        if(NumberLevel.Count != 0)
        {
            percentageCompleted = (totalCollected * 100 / NumberLevel.Count);
        }
        return percentageCompleted;
    }
}
