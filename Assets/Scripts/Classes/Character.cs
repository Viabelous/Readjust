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
    public float maxHp;

    [ReadOnly]
    public float hp;

    public float atk;
    public float def;
    public float agi, speed;
    public float foc;

    [HideInInspector]
    public bool actionEnabled = true, movementEnabled = true;


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