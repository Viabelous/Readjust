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
    public static List<GameObject> selectedSkills = new List<GameObject>()
    {
        // "Will Of Fire",
        // // "skill_sacrivert",
        // // "skill_fireball",
        // // "skill_explosion",
        // // "skill_ignite",
        // // "skill_nexus",
        // "Fudoshin",
        // "Preserve",
        // // "skill_sanare",
        // // "skill_avalanche",
        // "Invitro",
        // "Calm",
        // // "skill_lenire",
        // "Hydro Pulse",
        // // "skill_pebble_creation",
        // // "skill_thorn_cover",
        // // "skill_stalactite_shoot",
        // // "skill_waterwall",
        // // "skill_heavy_tide",
        // // "skill_javelin",
        // // "skill_wind_slash",
        // // "skill_whirlwind",
        // // "skill_landside_typhoon",

    };

    public static List<string> selectedItems = new List<string>();

    // public static

}