using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public enum SkillName
{
    BasicStab, Sacrivert, WillOfFire, Explosion, Ignite, Whirlwind, Fireball, HighTide, Waterwall,
    PeebleCreation, Avalanche
}

public enum Element
{
    None,
    Fire,
    Earth,
    Water,
    Air
}

public enum SkillType
{
    BurstDamage, CrowdControl, Buff, Healing, Debuff
}

public enum SkillHitType
{
    Once, Temporary
}

public enum SkillCost
{
    None, Mana, Hp
}

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField] private string id;            // id skill
    [SerializeField] private new string name;      // nama skill
    [SerializeField] private Element element;
    [SerializeField] private SkillType type;       // tipe damage yg diberikan
    [SerializeField] private SkillHitType hitType; // tipe pukulan yg diberikan
    [SerializeField] private SkillCost costType;   // tipe bayaran yg dipake
    [SerializeField] private float maxCd;          // cd maksimal
    [SerializeField] private float cost;           // total mana/hp di awal
    [SerializeField] private float damage;         // total damage di awal

    [Header("Skill Icon")]
    [SerializeField] private Sprite sprite;

    [Header("Linear Skill")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementRange;

    [Header("Temporary Skill")]
    [SerializeField] private float timer;

    [Header("Buff/Heal/Debuff")]
    [SerializeField] private float value;
    [SerializeField] private float persentase;

    [Header("Crowd Control")]
    [SerializeField] private float pushSpeed;
    [SerializeField] private float pushRange;

    private List<string> enemiesId = new List<string>();
    private int level = 1;

    private Transform lockedEnemy;


    public virtual void Activate(GameObject gameObject)
    {
        // switch (element)
        // {
        //     case Element.Fire:
        //         damage += 20;
        //         break;
        //     case Element.Earth:
        //         damage += 5;
        //         break;
        //     case Element.Water:
        //         damage += 10;
        //         break;
        //     case Element.Air:
        //         damage += 10;
        //         break;
        // }
    }

    public void HitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            enemiesId.Add(mobController.enemy.id);
        }
    }

    public void AfterHitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            enemiesId.Remove(mobController.enemy.id);
        }
    }

    public void ResetEnemiesId()
    {
        enemiesId.Clear();
    }

    public bool HasHit()
    {
        if (enemiesId.Count == 0)
        {
            return false;
        }
        return true;
    }

    public bool HasHitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            return enemiesId.Contains(mobController.enemy.id);
        }
        return false;
    }


    public Skill Clone()
    {
        return (Skill)this.MemberwiseClone();
    }

    public string Id
    {
        get
        {
            return id;
        }
    }

    public string Name
    {
        get { return name; }
    }

    public Element Element
    {
        get { return element; }
    }

    public SkillType Type
    {
        get { return type; }
    }

    public SkillHitType HitType
    {
        get { return hitType; }
    }

    public SkillCost CostType
    {
        get { return costType; }
    }

    public float Cd
    {
        get
        {
            switch (level)
            {
                case 1:
                    return maxCd - 0.1f * maxCd;
                case 2:
                    return maxCd - 0.2f * maxCd;
                case 3:
                    return maxCd - 0.3f * maxCd;
                default:
                    return maxCd;
            }
        }
    }

    public float Cost
    {
        get
        {
            return cost;
        }
    }

    public float Damage
    {
        get
        {
            switch (level)
            {
                case 1:
                    return damage + 0.1f * damage;
                case 2:
                    return damage + 0.2f * damage;
                case 3:
                    return damage + 0.3f * damage;
                default:
                    return damage;
            }
        }

        set
        {
            damage = value;
        }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }

    public float MovementRange
    {
        get { return movementRange; }
    }

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    public float Value
    {
        get { return value; }
    }

    public float Persentase
    {
        get { return persentase; }
    }

    public float PushSpeed
    {
        get { return pushSpeed; }
    }
    public float PushRange
    {
        get { return pushRange; }
    }

    public int Level
    {
        get { return level; }
    }

    public Transform LockedEnemy
    {
        get { return lockedEnemy; }
        set { lockedEnemy = value; }
    }
}
