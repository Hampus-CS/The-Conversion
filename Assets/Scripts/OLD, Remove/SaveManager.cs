using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class SaveManager
{

    private static readonly string saveFolder = Application.persistentDataPath + "/GameData";

    public static SaveProfile<T> Load<T>(string profileName) where T : SaveProfileData
    {

        if (!File.Exists($"{saveFolder}/{profileName}"))
            throw new Exception($"Save Profile {profileName} not found!");

        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        //decrypt
        Debug.Log($"Successfully loaded {saveFolder}/{profileName}");
        return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
    }

    public static void Save<T>(SaveProfile<T> save) where T : SaveProfileData
    {

        var jsonString = JsonConvert.SerializeObject(save, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        //encrypt
        if (!Directory.Exists(saveFolder)) //Create /GameData if its not already there!
            Directory.CreateDirectory(saveFolder);
        File.WriteAllText($"{saveFolder}/{save.profileName}", jsonString);
        Debug.Log($"Successfully saved/updated {saveFolder}/{save.profileName}");

    }

    public static void Delete(string profileName)
    {
        if (!File.Exists($"{saveFolder}/{profileName}"))
            throw new Exception($"Save Profile {profileName} not found!");
        Debug.Log($"Successfully deleted {saveFolder}/{profileName}");
        string filePath = $"{saveFolder}/{profileName}";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Successfully deleted {filePath}");
        }
        else
        {
            Debug.LogWarning($"Attempted to delete non-existing profile: {profileName}");
        }

    }

    public static bool SaveExists(string profileName)
    {
        return File.Exists($"{saveFolder}/{profileName}");
    }

}