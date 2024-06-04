using System.Collections.Generic;
using UnityEngine;
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
    public static List<string> unlockedItems = new List<string>();

    // high score tiap map
    public static List<Score> scores = new List<Score>();

    // skill yang dipakai selama stage berlangsung
    public static List<GameObject> selectedSkills = new List<GameObject>();

    // item yang dipakai selama stage berlangsung
    public static List<Item> selectedItems = new List<Item>();

    // pertemuan pertama dengan NPC
    public static Dictionary<string, bool> firstEncounter = new Dictionary<string, bool>()
                                                            {
                                                                {"Rion", true},
                                                                {"Zey", true},
                                                                {"Xiena", true},
                                                                {"Ken", true}
                                                            };

    public static bool CheckUnlockedSkill(string name)
    {
        return unlockedSkills.ContainsKey(name);
    }

    public static bool CheckUnlockedItems(string name)
    {
        return unlockedItems.Contains(name);
    }

    public static bool CheckSelectedItems(Item item)
    {
        return selectedItems.Contains(item);
    }

    public static void ResetData()
    {
        player = null;
        selectedMap = Map.None;
        scores.Clear();
        unlockedSkills.Clear();
        unlockedItems.Clear();
        selectedItems.Clear();
        selectedSkills.Clear();
        firstEncounter.Clear();
    }

    public static void SaveHistory(Score score)
    {
        scores.Add(score);
    }


}