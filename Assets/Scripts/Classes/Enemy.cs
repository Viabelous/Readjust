using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyName
{
    mob
}

public class Enemy : Character
{

    public EnemyName enemyName;
    public float aerusValue;
    public float expValue;

    public Enemy(EnemyName enemyName, float maxHp, float atk, float def, float agi, float foc, float aerusValue, float expValue)
    {
        this.enemyName = enemyName;
        this.maxHp = maxHp;
        this.hp = this.maxHp;
        this.atk = atk;
        this.def = def;
        this.agi = agi;
        this.foc = foc;
    }

    public float movementSpeed
    {
        get
        {
            // Isi dengan logika atau nilai yang ingin Anda kembalikan
            return 1 + agi * 0.1f;

        }
    }


}