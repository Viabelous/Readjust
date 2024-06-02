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

public enum SkillClass
{
    Basic,
    Intermediate,
    High,
    Supreme
}

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField] protected string id;            // id skill
    [SerializeField] protected new string name;      // nama skill
    [SerializeField] protected SkillClass classTier;
    [SerializeField] protected Element element;
    [SerializeField] protected SkillType type;       // tipe damage yg diberikan
    [SerializeField] protected SkillHitType hitType; // tipe pukulan yg diberikan 
    [SerializeField] protected SkillMovementType movementType; // tipe gerakan skill 
    [SerializeField] protected CostType costType;   // tipe bayaran yg dipake
    [SerializeField] protected float initCd;          // cd maksimal
    protected float maxCd = 0.5f;          // cd maksimal
    [SerializeField] protected float cost;           // total mana/hp di awal
    [SerializeField] protected float damage;         // total damage di awal

    [Header("Upgrade Skill")]
    [SerializeField] protected float upCd;
    [SerializeField] protected float upCost;


    [Header("Skill Icon")]
    [SerializeField] protected Sprite sprite;

    [Header("Linear Skill")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float movementRange;

    [Header("Temporary Skill")]
    [SerializeField] protected float timer;


    [Header("Description")]
    protected string description;
    protected int level = 1;
    protected int maxLevel = 10;

    [Header("Price")]
    protected float expUnlockCost;
    protected float expUpCost;


    protected List<string> enemiesId = new List<string>();
    protected Transform lockedEnemy;

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
            float finalCd = initCd + upCd * (Level - 1);
            return finalCd < maxCd ? maxCd : finalCd;
        }
    }

    // karena cost untuk sacrivert berbeda sehingga bisa diedit
    public float Cost
    {
        get
        {
            return cost + upCost * (Level - 1);
        }
    }

    public float ExpUpCost
    {
        get
        {
            switch (classTier)
            {
                case SkillClass.Basic:
                    this.expUpCost = 50;
                    break;

                case SkillClass.Intermediate:
                    this.expUpCost = 500;
                    break;

                case SkillClass.High:
                    this.expUpCost = 1000;
                    break;

                case SkillClass.Supreme:
                    this.expUpCost = 2000;
                    break;
            }
            return expUnlockCost + expUpCost * level;
        }
    }

    public float ExpUnlockCost
    {
        get
        {
            switch (classTier)
            {
                case SkillClass.Basic:
                    this.expUnlockCost = 100;
                    break;

                case SkillClass.Intermediate:
                    this.expUnlockCost = 1500;
                    break;

                case SkillClass.High:
                    this.expUnlockCost = 5000;
                    break;

                case SkillClass.Supreme:
                    this.expUnlockCost = 12500;
                    break;
            }
            return expUnlockCost;
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
    }

    public int Level
    {
        get { return level; }
    }
    public int MaxLevel
    {
        get { return maxLevel; }
    }

    public Transform LockedEnemy
    {
        get { return lockedEnemy; }
        set { lockedEnemy = value; }
    }

    public bool CanBeUnlocked(Player player)
    {
        switch (element)
        {
            case Element.None:
                return true;

            case Element.Fire:
                switch (classTier)
                {
                    case SkillClass.Basic:
                        return true;

                    case SkillClass.Intermediate:
                        if (player.GetProgress(Player.Progress.FireSkill) >= 2)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.High:
                        if (player.GetProgress(Player.Progress.FireSkill) >= 4)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.Supreme:
                        if (player.GetProgress(Player.Progress.FireSkill) >= 5)
                        {
                            return true;
                        }
                        break;
                }
                break;

            case Element.Earth:
                switch (classTier)
                {
                    case SkillClass.Basic:
                        return true;


                    case SkillClass.Intermediate:
                        if (player.GetProgress(Player.Progress.EarthSkill) >= 4)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.High:
                        if (player.GetProgress(Player.Progress.EarthSkill) >= 4 + 2)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.Supreme:
                        if (player.GetProgress(Player.Progress.EarthSkill) >= 4 + 2 + 1)
                        {
                            return true;
                        }
                        break;
                }
                break;

            case Element.Water:
                switch (classTier)
                {
                    case SkillClass.Basic:
                        return true;


                    case SkillClass.Intermediate:
                        if (player.GetProgress(Player.Progress.WaterSkill) >= 3)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.High:
                        if (player.GetProgress(Player.Progress.WaterSkill) >= 3 + 1)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.Supreme:
                        if (player.GetProgress(Player.Progress.WaterSkill) >= 3 + 1 + 1)
                        {
                            return true;
                        }
                        break;
                }
                break;

            case Element.Air:
                switch (classTier)
                {
                    case SkillClass.Basic:
                        return true;

                    case SkillClass.Intermediate:
                        if (player.GetProgress(Player.Progress.AirSkill) >= 3)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.High:
                        if (player.GetProgress(Player.Progress.AirSkill) >= 3 + 1)
                        {
                            return true;
                        }
                        break;
                    case SkillClass.Supreme:
                        if (player.GetProgress(Player.Progress.AirSkill) >= 3 + 1 + 1)
                        {
                            return true;
                        }
                        break;
                }
                break;

        }
        return false;

    }

    public bool CanBeUpgraded()
    {
        if (Level == MaxLevel)
        {
            return false;
        }
        return true;
    }

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

        // ubah state slot skill
        StartCooldown();
    }

    public void StartCooldown()
    {
        int index = GameManager.selectedSkills.FindIndex(skillPref => Name == skillPref.GetComponent<SkillController>().skill.Name);
        GameObject.Find("slot_" + (index + 1)).GetComponent<SkillUsage>().ChangeState(SkillState.Active);
    }

    public void PayWithCostType(Player player)
    {
        player.Pay(CostType, Cost);
    }

    public void UpgradeLevel()
    {
        this.level += 1;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }



    public virtual float GetDamage(Player player)
    {
        return damage;
    }

    public virtual string GetDescription()
    {
        return description;
    }

    public void RandomizeId()
    {
        this.id += Random.Range(0, 99999);
    }

    protected int PersentaseToInt(float persentase)
    {
        return Mathf.FloorToInt(persentase * 100);
    }
}
