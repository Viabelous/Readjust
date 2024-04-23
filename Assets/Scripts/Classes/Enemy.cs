using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyName
{
    PinkBoogie, YellowBoogie, FlamingBird, DesertAnomaly
}

[CreateAssetMenu]
public class Enemy : Character
{

    public EnemyName enemyName;
    public float aerusValue;
    public float expValue;

    private void OnEnable()
    {
        this.hp = this.maxHp;
    }

    public float movementSpeed
    {
        get
        {
            return speed + agi * 0.1f;
        }
    }

    public Enemy CloneObject()
    {
        Enemy newEnemy = ScriptableObject.CreateInstance<Enemy>();
        newEnemy.enemyName = this.enemyName;
        newEnemy.maxHp = this.maxHp;
        newEnemy.hp = this.hp;
        newEnemy.atk = this.atk;
        newEnemy.def = this.def;
        newEnemy.agi = this.agi;
        newEnemy.speed = this.speed;
        newEnemy.foc = this.foc;
        newEnemy.aerusValue = this.aerusValue;
        newEnemy.expValue = this.expValue;
        return newEnemy;
    }


}