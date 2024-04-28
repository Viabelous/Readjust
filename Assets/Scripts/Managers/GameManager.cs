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
        // GameData.unlockedSkills[3],
        // GameData.unlockedSkills[1],
        "skill_pebble",
        "skill_avalanche",
        "skill_ignite",
        "skill_explosion",
        "skill_fireball",
        // SkillName.WillOfFire,
        "skill_sacrivert",
        "skill_whirlwind",
        "skill_waterwall",
        // SkillName.HighTide,
    };

    // public static

}