using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    player,
    enemy
}

public class Character
{
    public float maxHp, hp;
    public float atk;
    public float def;
    public float agi;
    public float foc;
}