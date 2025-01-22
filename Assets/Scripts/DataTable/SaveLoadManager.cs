using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SaveDataVC = SaveDataV1;

public static class SaveLoadManager
{
    public static int SaveDataVersion { get; private set; } = 3;

    public static SaveDataVC Data {  get; set; }

    private static readonly string[] SaveFileName =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    private static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    private static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/Save";
        }
    }

    static SaveLoadManager()
    {
        if (!Load())
        {
            Data = new SaveDataVC();
            Save();
        }
    }

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileName.Length)
            return false;
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        var json = JsonConvert.SerializeObject(Data, settings);
        File.WriteAllText(path, json);
        return true;
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileName.Length)
            return false;

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        if (!File.Exists(path))
        {
            return false;
        }

        var json = File.ReadAllText(path);
        var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);
        while (saveData.Verison < SaveDataVersion)
        {
            saveData = saveData.VersionUp();
        }
        Data = saveData as SaveDataVC;
        return true;
    }
}
