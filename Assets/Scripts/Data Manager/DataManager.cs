using UnityEngine;
using System.IO;

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

        Debug.Log("Save data");
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(playerPath))
        {
            // FileStream stream = new FileStream(playerPath, FileMode.Open);

            string json = File.ReadAllText(playerPath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Data loaded from " + playerPath);

            // stream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning("No data file found at " + playerPath);
            return null;
        }
    }

    public static void SaveSkill(Player player)
    {

    }

    public static void SaveItem(Player player)
    {

    }



}