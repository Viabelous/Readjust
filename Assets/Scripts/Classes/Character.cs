using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    player,
    enemy
}

public class Character : ScriptableObject
{
    public float maxHp;

    [HideInInspector]
    public float hp;

    public float atk;
    public float def;
    public float agi, speed;
    public float foc;


}