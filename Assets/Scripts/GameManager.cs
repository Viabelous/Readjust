using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class GameManager
{
    // public static Sprite[] skillImgs = {};
    // private static Object[] loadedAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Skills/");
    // public static Sprite[] skillImgs = System.Array.FindAll(loadedAssets, obj => obj is Sprite) as Sprite[];
    public static List<Skill> skills = new List<Skill>() {
        new Skill("ignite", 1, 1),
        new Skill("waterwall", 3, 5),
        new Skill("high_tide", 1, 1),
        new Skill("whirlwind", 0.5f, 1),
    };

    public static Player playerNow = new Player();


}