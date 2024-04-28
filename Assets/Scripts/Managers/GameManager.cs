using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    // kalau sudah ada pake item, property (hp, mana, dll ubah)
    // public static Player player = GameData.player;

    public static List<string> selectedSkills = new List<string>()
    {
        // "skill_will_of_fire",
        // "skill_sacrivert",
        "skill_fireball",
        // "skill_explosion",
        // "skill_ignite",
        "skill_pebble_creation",
        "skill_avalanche",
        "skill_sanare",
        "skill_waterwall",
        "skill_heavy_tide",
        "skill_whirlwind",

    };

    // public static

}