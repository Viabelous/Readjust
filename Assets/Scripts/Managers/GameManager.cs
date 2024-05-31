using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum GameState
{
    OnStage,
    OnDeveloperZone,

}


// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    // data player yang sedang bermain saat ini
    public static Player player;

    // map stage yang dipilih player
    public static Map selectedMap = Map.Stage1;

    // skill yang sudah dibuka
    public static Dictionary<string, int> unlockedSkills = new Dictionary<string, int>();

    // item yang sudah dibuka
    public static Dictionary<string, int> unlockedItems = new Dictionary<string, int>();

    // high score tiap map
    public static List<Score> scores = new List<Score>();

    // skill yang dipakai selama stage berlangsung
    public static List<GameObject> selectedSkills = new List<GameObject>();

    // item yang dipakai selama stage berlangsung
    public static List<Item> selectedItems = new List<Item>();

    public static bool CheckUnlockedSkill(string name)
    {
        return unlockedSkills.ContainsKey(name);
    }

    public static void ResetData()
    {
        player = null;
        selectedMap = Map.None;
        scores.Clear();
        unlockedSkills.Clear();
        selectedItems.Clear();
        selectedSkills.Clear();
    }

    public static void SaveHistory(Score score)
    {
        scores.Add(score);
        // Dictionary<DateTime, List<float>> scoreValue = new Dictionary<DateTime, List<float>>();
        // scoreValue.Add(DateTime.Now, new List<float>() { score, time, status });
        // scores.Add(map.ToString(), scoreValue);
    }


}