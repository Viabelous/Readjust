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

[CreateAssetMenu(menuName = "Enemy/Common Enemy")]

public class Enemy : Character
{
    [Header("Enemy")]
    public new string name;
    public EnemyType type;
    [SerializeField] protected float aerus;
    [SerializeField] protected float exp;

    private void OnEnable()
    {
        this.id = "enemy" + UnityEngine.Random.Range(1, 99999).ToString();
        this.hp = this.maxHp;
    }

    public float MovementSpeed
    {
        get
        {
            return this.speed + this.agi * 0.1f;
        }
    }
    public override float GetMaxHP()
    {
        return this.maxHp;
    }

    public override float GetHP()
    {
        return this.hp;
    }
    public override float GetATK()
    {
        return this.atk;
    }
    public override float GetDEF()
    {
        return this.def;
    }
    public override float GetFOC()
    {
        return this.foc;
    }
    public override float GetAGI()
    {
        return this.agi;
    }
    public override float GetSpeed()
    {
        return this.speed;
    }
    public float GetAerus()
    {
        return this.aerus;
    }
    public float GetExp()
    {
        return this.exp;
    }


    public Enemy Clone()
    {
        Enemy newEnemy = (Enemy)this.MemberwiseClone();
        newEnemy.id = "enemy" + UnityEngine.Random.Range(1, 99999).ToString();
        return newEnemy;
    }

    public virtual void Spawning(GameObject gameObject)
    {

    }

    public virtual void OnAttacking(GameObject gameObject)
    {

    }

    public virtual void AnimationEvent()
    {

    }

    public override void Heal(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.HP:
                if (this.hp + value > this.maxHp)
                {
                    this.hp = this.maxHp;
                }
                else
                {
                    this.hp += value;
                }
                break;
        }
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
    //     return this.newEnemy;
    // }


}