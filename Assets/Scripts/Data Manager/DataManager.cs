using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class DataManager
{
    private static string path = "D:/ReadjustData/";
    private static string playerPath = path + "player.json";
    private static string skillPath = path + "skills.json";

    public static void SavePlayer(Player player)
    {
        // buat direktori jika belum ada
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        PlayerData data = new PlayerData(player);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(playerPath, json);

        // Debug.Log("Save data");
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(playerPath))
        {
            // FileStream stream = new FileStream(playerPath, FileMode.Open);

            string json = File.ReadAllText(playerPath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            // Debug.Log("Data loaded from " + playerPath);

            // stream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning("No data file found at " + playerPath);
            return null;
        }
    }

    public static void SaveSkills()
    {
        // buat direktori jika belum ada
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        SkillData data = new SkillData();
        string json = JsonConvert.SerializeObject(data.skills, Formatting.Indented);
        File.WriteAllText(playerPath, json);
    }

    public static SkillData LoadSkills()
    {
        if (File.Exists(skillPath))
        {
            try
            {
                string json = File.ReadAllText(skillPath);
                Dictionary<string, int> dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

                SkillData skillData = new SkillData();
                skillData.skills = dict;

                Debug.Log("Dictionary data loaded from " + skillPath);
                return skillData;
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to load dictionary data: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("No dictionary data file found at " + skillPath);
            return null;
        }
    }

    public static void SaveItem(Player player)
    {

    }



}