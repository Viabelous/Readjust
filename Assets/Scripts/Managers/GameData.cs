using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// digunakan pada data-data di luar stage
public class GameData : ScriptableObject
{

    public static List<Skill> skills = new List<Skill>() {
        new Skill("sacrivert", 0, 1, 1),
        new Skill("ignite", 30, 1, 1),
        new Skill("fireball", 20, 0.5f, 1),
        new Skill("explosion", 20, 0.5f, 1),
        new Skill("waterwall", 10, 3, 5),
        new Skill("high_tide", 15, 1, 1),
        new Skill("whirlwind", 20, 0.5f, 1),
    };

    public static List<Skill> unlockedSkills = new List<Skill>() {
        skills[0], skills[1], skills[2], skills[3],  skills[4], skills[5]
    };

    public static Player ogPlayer = new Player(
        200, // hp
        100, // mana
        50,  // shield
        0,   // aerus
        1    // level
    );


    // public static List<int> stages = new List<int>() { 1, 2, 3, 4, 5 };
    // public static List<int> unlockedStages = new List<int>() { 1 };

    // public static List<float> timeHistory = new List<float>();


}