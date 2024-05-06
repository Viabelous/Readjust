using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// public enum EnemyName
// {
//     PinkBoogie, YellowBoogie, FlamingBird, DesertAnomaly
// }

public enum EnemyType
{
    Ground,
    Flying
}

[CreateAssetMenu]
public class Enemy : Character
{
    [Header("Enemy")]
    public new string name;
    public EnemyType type;
    public float aerusValue;
    public float expValue;

    private void OnEnable()
    {
        this.id = "enemy" + UnityEngine.Random.Range(1, 99999).ToString();
        this.hp = this.maxHp;
    }

    public float movementSpeed
    {
        get
        {
            return speed + agi * 0.1f;
        }
    }

    public Enemy Clone()
    {
        Enemy newEnemy = (Enemy)this.MemberwiseClone();
        newEnemy.id = "enemy" + UnityEngine.Random.Range(1, 99999).ToString();
        return newEnemy;
    }


    // public Enemy CloneObject()
    // {
    //     Enemy newEnemy = ScriptableObject.CreateInstance<Enemy>();
    //     newEnemy.id = "enemy" + UnityEngine.Random.Range(1, 99999).ToString();
    //     newEnemy.name = this.name;
    //     newEnemy.maxHp = this.maxHp;
    //     newEnemy.hp = this.hp;
    //     newEnemy.atk = this.atk;
    //     newEnemy.def = this.def;
    //     newEnemy.agi = this.agi;
    //     newEnemy.speed = this.speed;
    //     newEnemy.foc = this.foc;
    //     newEnemy.aerusValue = this.aerusValue;
    //     newEnemy.expValue = this.expValue;
    //     return newEnemy;
    // }


}