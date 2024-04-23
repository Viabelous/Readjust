using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    // kalau sudah ada pake item, property (hp, mana, dll ubah)
    // public static Player player = GameData.player;

    public static List<SkillName> selectedSkills = new List<SkillName>()
    {
        // GameData.unlockedSkills[3],
        // GameData.unlockedSkills[1],
        SkillName.Ignite,
        SkillName.Explosion,
        SkillName.Fireball,
        SkillName.WillOfFire,
        SkillName.Sacrivert,
        SkillName.Whirlwind,
        SkillName.Waterwall,
        // SkillName.HighTide
        // GameData.unlockedSkills[2],
        // GameData.unlockedSkills[4],
        // GameData.unlockedSkills[5],
        // GameData.unlockedSkills[6],
    };

    // public static

}