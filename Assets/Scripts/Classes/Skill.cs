using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

enum SkillState
{
    ready,
    cooldown
}
public class Skill
{
    public string name;
    public float maxCd;
    public float manaUsage;
    public float damage;

    public bool isCooldown = false;
    // public GameObject skillObj;
    public Sprite sprite;


    public Skill(string name, float damage, float maxCd, float manaUsage)
    {
        this.name = name;
        this.damage = damage;
        this.maxCd = maxCd;
        this.manaUsage = manaUsage;
        this.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Skills/" + name + ".png");
    }


    // public void Attack()
    // {
    //     switch (name)
    //     {
    //         case "ignite":
    //             skillObj.GetComponent<IgniteSkill>().Active();
    //             isCooldown = true;
    //             break;

    //         case "waterwall":
    //             skillObj.GetComponent<WaterwallSkill>().Active();
    //             isCooldown = true;

    //             break;

    //         case "high_tide":
    //             skillObj.GetComponent<HighTideSkill>().Active();
    //             isCooldown = true;

    //             break;

    //         case "whirlwind":
    //             skillObj.GetComponent<WhirlwindSkill>().Active();
    //             isCooldown = true;

    //             break;
    //     }
    // }



    // public void 
}