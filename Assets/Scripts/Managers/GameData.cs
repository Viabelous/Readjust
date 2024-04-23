using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// digunakan pada data-data di luar stage
public class GameData : ScriptableObject
{

    // public static List<Skill> skills = new List<Skill>() {
    //     new Skill("sacrivert", 0, 1, 1),
    //     new Skill("will_of_fire", 0, 1, 1),
    //     new Skill("ignite", 30, 1, 1),
    //     new Skill("fireball", 20, 0.5f, 1),
    //     new Skill("explosion", 20, 0.5f, 1),
    //     new Skill("waterwall", 10, 3, 5),
    //     new Skill("high_tide", 15, 1, 1),
    //     new Skill("whirlwind", 20, 0.5f, 1),
    // };
    public static List<SkillName> skills = new List<SkillName>() {
        SkillName.basicStab,
        SkillName.sacrivert,
        SkillName.explosion,
        SkillName.ignite,
        SkillName.fireball,
        SkillName.whirlwind,
        SkillName.highTide,
        SkillName.waterwall
    };

    public static List<SkillName> unlockedSkills = new List<SkillName>() {
SkillName.ignite,
    };

    public static Player player = new Player(
        100, // hp
        100, // mana
        10,  // atk
        5,   // def
        10,  // agi
        5    // foc
    );

    public static List<Enemy> enemies = new List<Enemy>() {
        new Enemy(
            EnemyName.mob,
            15, // hp
            5,  // atk
            0,  // def
            0,  // agi
            0,  // foc
            5,  // aerus
            1   // exp
        )
    };


    // public static List<int> stages = new List<int>() { 1, 2, 3, 4, 5 };
    // public static List<int> unlockedStages = new List<int>() { 1 };

    // public static List<float> timeHistory = new List<float>();


}