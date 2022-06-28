using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string DataDirPath = "";
    private string DataFileName = "";

    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";
    public FileDataHandler(string DataDirPath, string DataFileName, bool useEncrytion)
    {
        this.DataDirPath = DataDirPath;
        this.DataFileName = DataFileName;
        this.useEncryption = useEncrytion;
    }
    public GameData Load(string profileId)
    {
        string fullPath = Path.Combine(DataDirPath,profileId, DataFileName);
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
                if (useEncryption)
                {
                    dataLoad = EncryptDecrypt(dataLoad);
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
    public void Save(GameData data, string profileId)
    {
        string fullPath = Path.Combine(DataDirPath,profileId, DataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            String dataToStore = JsonUtility.ToJson(data, true);
            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
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
    public Dictionary<string,GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(DataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(DataDirPath, profileId, DataFileName);
            if(!File.Exists(fullPath))
            {
                Debug.LogWarning("skipping :" + profileId);
                continue;
            }
            GameData profileData = Load(profileId);
            if(profileId != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("tried:" + profileId);
            }
        }
        return profileDictionary;
    }
    private string EncryptDecrypt(string data)
    {
        string modifieData = "";
        for(int i=0;i<data.Length;i++)
        {
            modifieData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifieData;
    }
}
