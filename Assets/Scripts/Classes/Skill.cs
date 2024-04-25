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

public class Skill : ScriptableObject
{
    public new SkillName name;
    public SkillType type;
    public SkillHitType hitType;
    public SkillCost costType;
    public float maxCd;
    public float cost;
    public float damage;

    [Header("Linear Skill")]
    public float movementSpeed;
    public float movementRange;

    [Header("Temporary Skill")]
    public float timer;

    [Header("Skill Icon")]
    public Sprite sprite;
    private List<string> enemies = new List<string>();

    public virtual void Activate(GameObject gameObject)
    {

    }


    // public virtual void HitEnemyFirstTime(GameObject gameObject, Collider2D other)
    // {

    // }

    public virtual void HitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            enemies.Add(mobController.enemy.id);
        }
    }

    public virtual void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            enemies.Remove(mobController.enemy.id);
        }
    }

    public virtual bool HasHitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            return enemies.Contains(mobController.enemy.id);
        }
        return false;
    }

}
