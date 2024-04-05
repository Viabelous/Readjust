using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;



public class Skill
{
    public string name;
    public float maxCd;
    public bool isCooldown = false;
    public GameObject skillObj;
    public Sprite sprite;

    public Skill(string name, float maxCd)
    {
        this.name = name;
        this.maxCd = maxCd;
        this.skillObj = GameObject.Find(name);
        this.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Skills/" + name + ".png");
    }


    public void Attack()
    {
        if (isCooldown)
        {
            return;
        }

        switch (name)
        {
            case "ignite":
                skillObj.GetComponent<IgniteSkill>().Active();
                isCooldown = true;
                break;

            case "waterwall":
                skillObj.GetComponent<WaterwallSkill>().Active();
                isCooldown = true;

                break;

            case "high_tide":
                skillObj.GetComponent<HighTideSkill>().Active();
                isCooldown = true;

                break;

            case "whirlwind":
                skillObj.GetComponent<WhirlwindSkill>().Active();
                isCooldown = true;

                break;
        }
    }



    // public void 
}