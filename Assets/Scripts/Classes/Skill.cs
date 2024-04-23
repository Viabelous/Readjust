using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;



public class Skill : ScriptableObject
{
    public new SkillName name;
    public SkillType type;
    public SkillHitType hitType;
    public SkillCost costType;
    public float maxCd;
    public float cost;
    public float damage;
    public string description;

    public Sprite sprite;

    public virtual void Activate(GameObject gameObject)
    {

    }


    public virtual void HitEnemyFirstTime(GameObject gameObject, Collider2D other)
    {

    }

    public virtual void HitEnemy(GameObject gameObject, Collider2D other)
    {

    }

    public virtual void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {

    }

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

public enum SkillHitType
{
    once,
    temporary
}

public enum SkillCost
{
    none,
    mana,
    hp
}