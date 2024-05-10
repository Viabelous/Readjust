using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum GameState
{
    OnStage,
    OnDeveloperZone
}

// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    // kalau sudah ada pake item, property (hp, mana, dll ubah)
    // public static Player player = GameData.player;

    // nama skillnya
    public static List<GameObject> selectedSkills = new List<GameObject>();

    public static List<Item> selectedItems = new List<Item>();

    // public static

}