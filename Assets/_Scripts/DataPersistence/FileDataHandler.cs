using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string DataDirPath = "";
    private string DataFileName = "";

    public FileDataHandler(string DataDirPath, string DataFileName)
    {
        this.DataDirPath = DataDirPath;
        this.DataFileName = DataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(DataDirPath, DataFileName);
        GameData LoadData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string dataLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataLoad = reader.ReadToEnd();
                    }
                }
                LoadData = JsonUtility.FromJson<GameData>(dataLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured:" + fullPath + "\n" + e);
            }
        }
        return LoadData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(DataDirPath, DataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            String dataToStore = JsonUtility.ToJson(data, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured:" + fullPath + "\n" + e);
        }
    }
}
