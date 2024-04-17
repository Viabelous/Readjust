using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class GameManager
{
    // public static Sprite[] skillImgs = {};
    // private static Object[] loadedAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Skills/");
    // public static Sprite[] skillImgs = System.Array.FindAll(loadedAssets, obj => obj is Sprite) as Sprite[];
    public static List<Skill> skills = new List<Skill> {
        new Skill("ignite", 1, 3),
        new Skill("waterwall", 3, 10),
        new Skill("high_tide", 4, 15),
        new Skill("whirlwind", 2, 5),
    };

    public static Player playerNow = new Player();


}