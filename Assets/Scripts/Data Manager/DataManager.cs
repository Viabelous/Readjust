using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class DataManager
{
    private static string path = "D:/ReadjustData/";
    private static string playerPath = path + "player.json";
    private static string skillPath = path + "skills.json";

    // public static void SavePlayer(Player player)
    // {
    //     // buat direktori jika belum ada
    //     if (!Directory.Exists(path))
    //     {
    //         Directory.CreateDirectory(path);
    //     }

    //     PlayerData data = new PlayerData(player);

    //     string json = JsonUtility.ToJson(data);
    //     File.WriteAllText(playerPath, json);
    // }

    public static void SavePlayer(Player player)
    {
        // buat direktori jika belum ada
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string jsonData = JsonConvert.SerializeObject(
            player.DataToJson(),
            Formatting.Indented,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }
        );
        File.WriteAllText(playerPath, jsonData);
    }

    public static Dictionary<string, object> LoadPlayer()
    {
        if (File.Exists(playerPath))
        {
            string jsonData = File.ReadAllText(playerPath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return data;
        }
        else
        {
            Debug.LogWarning("No player data file found at " + playerPath);
            return null;
        }
    }

    // public static PlayerData LoadPlayer()
    // {
    //     if (File.Exists(playerPath))
    //     {
    //         string json = File.ReadAllText(playerPath);
    //         PlayerData data = JsonUtility.FromJson<PlayerData>(json);
    //         return data;
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No player data file found at " + playerPath);
    //         return null;
    //     }
    // }

    public static void SaveSkills(Dictionary<string, int> dictData)
    {
        // buat direktori jika belum ada
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonConvert.SerializeObject(dictData, Formatting.Indented);
        File.WriteAllText(skillPath, json);
    }

    public static Dictionary<string, int> LoadSkills()
    {
        if (File.Exists(skillPath))
        {
            try
            {
                string json = File.ReadAllText(skillPath);
                Dictionary<string, int> dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                return dict;
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to load skills data: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("No skills data file found at " + skillPath);
            return null;
        }
    }

    public static void SaveItem(Player player)
    {

    }

    public static bool CheckPath()
    {
        if (File.Exists(playerPath) && File.Exists(skillPath))
        {
            return true;
        }
        return false;
    }

}