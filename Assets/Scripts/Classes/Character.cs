using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public enum CharacterType
{
    Player,
    Enemy,
    FlyingEnemy
}

public enum Stat
{
    HP,
    Mana,

    ATK,
    DEF,
    FOC,
    AGI,
    Shield
}

public class Character : ScriptableObject
{
    public string id;
    [SerializeField] protected float maxHp;

    [HideInInspector] public float hp;

    [SerializeField] protected float atk;
    [SerializeField] protected float def;
    [SerializeField] protected float agi, speed;
    [SerializeField] protected float foc;

    [HideInInspector]
    public bool actionEnabled = true, movementEnabled = true;

    public virtual float GetMaxHP()
    {
        return maxHp;
    }

    public virtual float GetHP()
    {
        return hp;
    }
    public virtual float GetATK()
    {
        return atk;
    }
    public virtual float GetDEF()
    {
        return def;
    }
    public virtual float GetFOC()
    {
        return foc;
    }
    public virtual float GetAGI()
    {
        return agi;
    }
    public virtual float GetSpeed()
    {
        return speed;
    }


    public virtual void Upgrade(Stat stat, float value)
    {

    }

    public virtual void Downgrade(Stat stat, float value)
    {

    }

    public virtual void Heal(Stat stat, float value)
    {

    }


}