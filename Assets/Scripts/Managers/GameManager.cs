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
        SkillName.PeebleCreation,
        SkillName.Avalanche,
        SkillName.Ignite,
        SkillName.Explosion,
        SkillName.Fireball,
        // SkillName.WillOfFire,
        SkillName.Sacrivert,
        SkillName.Whirlwind,
        SkillName.Waterwall,
        // SkillName.HighTide,
    };

    // public static

}