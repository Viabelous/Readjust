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
        // "skill_fireball",
        // "skill_explosion",
        // "skill_ignite",
        // "skill_fudoshin",
        "skill_landside_typhoon",
        "skill_wind_slash",
        "skill_preserve",
        "skill_sanare",
        // "skill_avalanche",
        "skill_lenire",
        // "skill_pebble_creation",
        // "skill_thorn_cover",
        "skill_stalactite_shoot",
        "skill_invitro",
        // "skill_calm",
        
        // "skill_waterwall",
        // "skill_heavy_tide",
        // "skill_whirlwind",

    };

    // public static

}