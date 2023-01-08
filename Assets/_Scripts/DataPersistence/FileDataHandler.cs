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
    private readonly string backupExtension = ".bak";
    public FileDataHandler(string DataDirPath, string DataFileName, bool useEncrytion)
    {
        this.DataDirPath = DataDirPath;
        this.DataFileName = DataFileName;
        this.useEncryption = useEncrytion;
    }
    public GameData Load(string profileId, bool allowRestoreFromBackup = true)
    {
        if(profileId == null)
        {
            return null;
        }
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
                if(allowRestoreFromBackup)
                {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        LoadData = Load(profileId,false);
                    }
                }
                else
                {
                    Debug.LogError("Error occured when trying to load file at path:"
                        + fullPath + " and backup did not work.\n" + e);
                }
            }
        }
        return LoadData;
    }
    public void Save(GameData data, string profileId)
    {
        if(profileId == null)
        {
            return;
        }
        string fullPath = Path.Combine(DataDirPath,profileId, DataFileName);
        string backupFilePath = fullPath + backupExtension;
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
            GameData verifiedGameData = Load(profileId);
            if(verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            else
            {
                throw new Exception("Save file Could not be verified and backup could not be created. ");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured:" + fullPath + "\n" + e);
        }
    }
    public void Delete(string profileId)
    {
        if(profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(DataDirPath, profileId, DataFileName);
        try
        {
            if(File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath),true);
            }
            else
            {
                Debug.LogWarning(" Tried to delete profile data, but data was not found at path: " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to delete profile data for profileId: " + profileId + "at path"+ fullPath + "\n" + e);
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
    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;
        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach(KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;
            if(gameData == null)
            {
                continue;
            }
            if(mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                if(newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }
        return mostRecentProfileId;
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
    private bool AttemptRollback(string fullpath)
    {
        bool success = false;
        string backupFilePath = fullpath + backupExtension;
        try
        {
            if(File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullpath, true);
                success = true;
                Debug.LogWarning("had to roll back to backup file at: " + backupFilePath);
            }
            else
            {
                throw new Exception("Tried to roll back, but no backup file exists to roll back to.");
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when try to roll back to backup file at: "
                + backupFilePath + "\n" + e);
        }
        return success;
    }
}
