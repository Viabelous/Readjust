using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

// public enum SkillName
// {
//     BasicStab, Sacrivert, WillOfFire, Explosion, Ignite, Whirlwind, Fireball, HighTide, Waterwall,
//     PeebleCreation, Avalanche
// }

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

public enum CostType
{
    None, Mana, Hp, Aerus, Exp
}

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField] protected string id;            // id skill
    [SerializeField] protected new string name;      // nama skill
    [SerializeField] protected Element element;
    [SerializeField] protected SkillType type;       // tipe damage yg diberikan
    [SerializeField] protected SkillHitType hitType; // tipe pukulan yg diberikan 
    [SerializeField] protected SkillMovementType movementType; // tipe gerakan skill 
    [SerializeField] protected CostType costType;   // tipe bayaran yg dipake
    [SerializeField] protected float maxCd;          // cd maksimal
    [SerializeField] protected float cost;           // total mana/hp di awal
    [SerializeField] protected float damage;         // total damage di awal

    [Header("Skill Icon")]
    [SerializeField] protected Sprite sprite;

    [Header("Linear Skill")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float movementRange;

    [Header("Temporary Skill")]
    [SerializeField] protected float timer;
    protected int level = 1;

    // [HideInInspector] protected bool invalid = false;

    // [Header("Buff/Heal/Debuff")]
    // [SerializeField] private float value;
    // [SerializeField] private float persentase;

    // [Header("Crowd Control")]
    // [SerializeField] private float pushSpeed;
    // [SerializeField] private float pushRange;

    protected List<string> enemiesId = new List<string>();

    protected Transform lockedEnemy;

    public virtual void Activate(GameObject gameObject)
    {

    }
    public virtual void Deactivate(GameObject gameObject)
    {

    }

    public virtual void OnActivated(GameObject gameObject)
    {

    }

    public virtual void OnDeactivated(GameObject gameObject)
    {

    }

    public virtual void HitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mobController = other.GetComponent<MobController>();
            enemiesId.Add(mobController.enemy.id);
        }
    }

    public virtual void WhileHitEnemy(Collider2D other)
    {

    }

    public virtual void AfterHitEnemy(Collider2D other)
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

    public virtual Skill Clone()
    {
        Skill newSkill = (Skill)this.MemberwiseClone();
        newSkill.RandomizeId();
        return newSkill;
    }

    public void Payment(Transform player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        PayWithCostType(playerController.player);

        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!
        // int index = CumaBuatDebug.instance.selectedSkills.FindIndex(skillPref => Name == skillPref.GetComponent<SkillController>().skill.Name);

        // ubah state slot skill
        StartCooldown();
    }

    public void StartCooldown()
    {
        int index = GameManager.selectedSkills.FindIndex(skillPref => Name == skillPref.GetComponent<SkillController>().skill.Name);
        GameObject.Find("slot_" + index + 1).GetComponent<SkillUsage>().ChangeState(SkillState.Active);
    }

    public void PayWithCostType(Player player)
    {
        player.Pay(CostType, Cost);
    }

    // public void CancelSkill()
    // {
    //     invalid = true;
    // }

    // public bool Invalid()
    // {
    //     return invalid;
    // }

    // id dapat diubah setiap pembuatan objek baru
    // agar id tiap skill berbeda, biarpun objeknya sama
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
    public SkillMovementType MovementType
    {
        get { return movementType; }
    }

    public CostType CostType
    {
        get { return costType; }
    }

    public float Cd
    {
        get
        {
            return maxCd;
        }
    }

    // karena cost untuk sacrivert berbeda sehingga bisa diedit
    public float Cost
    {
        get
        {
            return cost;
        }
    }

    // karena ada tambahan damage tiap elemen tergantung stage sehingga bisa diedit
    // public float Damage
    // {
    //     get
    //     {
    //         // switch (level)
    //         // {
    //         //     case 1:
    //         //         return damage + 0.1f * damage;
    //         //     case 2:
    //         //         return damage + 0.2f * damage;
    //         //     case 3:
    //         //         return damage + 0.3f * damage;
    //         //     default:
    //         //         return damage;
    //         // }
    //         return damage;
    //     }

    //     set
    //     {
    //         damage = value;
    //     }
    // }

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

    public virtual float GetDamage(Character character)
    {
        return damage;
    }

    public void RandomizeId()
    {
        this.id += Random.Range(0, 99999);
    }
}
