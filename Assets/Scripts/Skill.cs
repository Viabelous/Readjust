using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;



public class Skill : ScriptableObject
{
    public new SkillName name;
    public SkillType type;
    public float maxCd;
    public float manaUsage;
    public float damage;
    public string description;

    public Sprite sprite;

    public virtual void Activate(GameObject gameObject)
    {

    }

    public virtual void HitEnemy(Collider2D other)
    {

    }

    public virtual void AfterHitEnemey(Collider2D other)
    {

    }

    // public virtual void OnTriggerEnter2D(Collider2D other)
    // {

    // }
    // public bool isCooldown = false;
    // public GameObject skillObj;


    // public Skill(string name, float damage, float maxCd, float manaUsage)
    // {
    //     this.name = name;
    //     this.damage = damage;
    //     this.maxCd = maxCd;
    //     this.manaUsage = manaUsage;
    //     // this.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Skills/" + name + ".png");
    // }


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

public enum SkillName
{
    basicStab,
    sacrivert,
    willOfFire,
    explosion,
    ignite,
    whirlwind,
    fireball,
    highTide,
    waterwall

}

public enum SkillType
{
    burstDamage, crowdControl, buff, healing, debuff
}