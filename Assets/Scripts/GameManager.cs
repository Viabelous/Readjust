using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class GameManager
{
    // public static Sprite[] skillImgs = {};
    // private static Object[] loadedAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Skills/");
    // public static Sprite[] skillImgs = System.Array.FindAll(loadedAssets, obj => obj is Sprite) as Sprite[];
    public static Skill[] skillsAvailable = {
        new Skill("ignite", 0),
        new Skill("waterwall", 0),
        new Skill("high_tide", 3),
        new Skill("whirlwind", 3),
    };
    public static Player playerNow = new Player();


}