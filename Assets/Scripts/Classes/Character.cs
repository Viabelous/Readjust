using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    Player,
    Enemy,
    FlyingEnemy
}

public class Character : ScriptableObject
{
    public string id;
    public float maxHp;

    [HideInInspector]
    public float hp;

    public float atk;
    public float def;
    public float agi, speed;
    public float foc;

    [HideInInspector]
    public bool actionEnabled = true, movementEnabled = true;




}