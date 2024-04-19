using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// digunakan dalam menuju on stage (?)
public class GameManager : ScriptableObject
{
    // kalau sudah ada pake item, property (hp, mana, dll ubah)
    public static Player player = GameData.ogPlayer;

    public static List<Skill> selectedSkills = new List<Skill>() {
        GameData.unlockedSkills[3],
        GameData.unlockedSkills[1],
        GameData.unlockedSkills[0],
        GameData.unlockedSkills[2],
        GameData.unlockedSkills[4],
        GameData.unlockedSkills[5],
    };

    // public static

}