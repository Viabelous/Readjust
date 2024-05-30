using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

public static class DataManager
{
    private static string path = "D:/ReadjustData/";
    private static string playerPath = path + "player.json";
    private static string skillsPath = path + "skills.json";
    private static string scoresPath = path + "scores.json";

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

    // public static void SaveScores(Dictionary<string, Dictionary<DateTime, List<float>>> scores)
    // {
    //     // buat direktori jika belum ada
    //     if (!Directory.Exists(path))
    //     {
    //         Directory.CreateDirectory(path);
    //     }

    //     string jsonData = JsonConvert.SerializeObject(
    //         scores,
    //         Formatting.Indented,
    //         new JsonSerializerSettings
    //         {
    //             TypeNameHandling = TypeNameHandling.Auto
    //         }
    //     );
    //     File.WriteAllText(scoresPath, jsonData);
    // }
    public static void SaveScores(List<Dictionary<string, object>> scores)
    {
        // buat direktori jika belum ada
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string jsonData = JsonConvert.SerializeObject(
            scores,
            Formatting.Indented,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }
        );
        File.WriteAllText(scoresPath, jsonData);
    }

    public static List<Dictionary<string, object>> LoadScores()
    {
        if (File.Exists(scoresPath))
        {
            string jsonData = File.ReadAllText(scoresPath);
            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return data;
        }
        else
        {
            Debug.LogWarning("No score data file found at " + scoresPath);
            return null;
        }
    }

    // public static Dictionary<string, Dictionary<DateTime, List<float>>> LoadScores()
    // {
    //     if (File.Exists(scoresPath))
    //     {
    //         string jsonData = File.ReadAllText(scoresPath);
    //         var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<DateTime, List<float>>>>(jsonData, new JsonSerializerSettings
    //         {
    //             TypeNameHandling = TypeNameHandling.Auto
    //         });
    //         return data;
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No score data file found at " + scoresPath);
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
        File.WriteAllText(skillsPath, json);
    }

    public static Dictionary<string, int> LoadSkills()
    {
        if (File.Exists(skillsPath))
        {
            try
            {
                string json = File.ReadAllText(skillsPath);
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
            Debug.LogWarning("No skills data file found at " + skillsPath);
            return null;
        }
    }

    public static void SaveItem(Player player)
    {

    }

    public static bool CheckPath()
    {
        if (File.Exists(playerPath) && File.Exists(skillsPath) && File.Exists(scoresPath))
        {
            return true;
        }
        return false;
    }

}