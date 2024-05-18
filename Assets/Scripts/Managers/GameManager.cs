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
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
}

// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    public static Player player;

    public static Map selectedMap = Map.Stage1;

    // nama skillnya
    public static List<GameObject> selectedSkills = new List<GameObject>();

    public static List<Item> selectedItems = new List<Item>();

    // public static

}