using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum GameState
{
    OnStage,
    OnDeveloperZone,

}

public enum Map
{
    None,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
}

// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    // data player yang sedang bermain saat ini
    public static Player player;

    // map stage yang dipilih player
    public static Map selectedMap = Map.None;

    // skill yang sudah dibuka
    public static Dictionary<string, int> unlockedSkills = new Dictionary<string, int>();

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
        unlockedSkills.Clear();
        selectedItems.Clear();
        selectedSkills.Clear();
    }


}