using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu]
public class Player : Character
{
    [SerializeField] protected float maxMana, hpRegen, manaRegen;

    [Header("Player Data (Don't edit this!)")]

    [ReadOnly]
    [SerializeField] public float mana;
    public float maxShield;
    public float shield = 0;
    public float aerus = 0;
    public float exp = 0;
    public float venetia = 0;

    private float regenTimer = 0;


    [Header("Stat Level")]
    [SerializeField] private float aerusUpCost;
    [SerializeField] private float expUpCost;
    private int maxHPLevel = 1;
    private int maxManaLevel = 1;
    private int atkLevel = 1;
    private int defLevel = 1;
    private int agiLevel = 1;
    private int focLevel = 1;
    private int statMaxLevel = 10;

    public enum Progress
    {
        Story, FireSkill, EarthSkill, WaterSkill, AirSkill,
        MaxHP, MaxMana, ATK, DEF, FOC, AGI
    }

    [Header("Progress")]
    private int story = 0;
    private int fireSkill = 0;
    private int earthSkill = 0;
    private int waterSkill = 0;
    private int airSkill = 0;

    private void OnEnable()
    {
        // Kode yang ingin dijalankan saat scriptable object diaktifkan pertama kali
        this.id = "player" + UnityEngine.Random.Range(1, 99999).ToString();
        this.hp = GetMaxHP();
        this.mana = GetMaxMana();
        this.shield = maxShield;
    }

    public float MovementSpeed
    {
        get { return this.speed + this.agi * 0.1f; }
    }
    public int StatMaxLevel
    {
        get { return this.statMaxLevel; }
    }

    // public int StatPriceUp {
    //     get { return this.statPriceUp * ();}
    // }

    public int GetProgress(Progress type)
    {
        switch (type)
        {
            case Progress.Story:
                return this.story;
            case Progress.FireSkill:
                return this.fireSkill;
            case Progress.EarthSkill:
                return this.earthSkill;
            case Progress.WaterSkill:
                return this.waterSkill;
            case Progress.AirSkill:
                return this.airSkill;
            case Progress.MaxHP:
                return this.maxHPLevel;
            case Progress.MaxMana:
                return this.maxManaLevel;
            case Progress.ATK:
                return this.atkLevel;
            case Progress.DEF:
                return this.defLevel;
            case Progress.FOC:
                return this.focLevel;
            case Progress.AGI:
                return this.agiLevel;
        }
        return 0;
    }
    public void IncreaseProgress(Progress type, int value)
    {
        switch (type)
        {
            case Progress.Story:
                this.story += value;
                break;
            case Progress.FireSkill:
                this.fireSkill += value;
                break;
            case Progress.EarthSkill:
                this.earthSkill += value;
                break;
            case Progress.WaterSkill:
                this.waterSkill += value;
                break;
            case Progress.AirSkill:
                this.airSkill += value;
                break;

            case Progress.MaxHP:
                Upgrade(Stat.HP, value);
                // this.maxHPLevel += value;
                break;

            case Progress.MaxMana:
                Upgrade(Stat.Mana, value);
                // this.maxManaLevel += value;
                break;

            case Progress.ATK:
                Upgrade(Stat.ATK, value);
                // this.atkLevel += value;
                break;

            case Progress.DEF:
                Upgrade(Stat.DEF, value);
                // this.defLevel += value;
                break;

            case Progress.FOC:
                Upgrade(Stat.FOC, value);
                // this.focLevel += value;
                break;

            case Progress.AGI:
                Upgrade(Stat.AGI, value);
                // this.agiLevel += value;
                break;
        }
    }

    public bool CanBeUpgraded(Progress type)
    {
        switch (type)
        {
            case Progress.MaxHP:
                if (maxHPLevel < 10)
                {
                    return true;
                }
                break;
            case Progress.MaxMana:
                if (maxManaLevel < 10)
                {
                    return true;
                }
                break;
            case Progress.ATK:
                if (atkLevel < 10)
                {
                    return true;
                }
                break;
            case Progress.DEF:
                if (defLevel < 10)
                {
                    return true;
                }
                break;
            case Progress.FOC:
                if (focLevel < 10)
                {
                    return true;
                }
                break;
            case Progress.AGI:
                if (agiLevel < 10)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public override float GetMaxHP()
    {
        return this.maxHp + 100 * maxHPLevel;
    }

    public override float GetHP()
    {
        return hp;
    }
    public float GetMaxMana()
    {
        return this.maxMana + 50 * maxManaLevel;
    }

    public float GetMana()
    {
        return this.mana;
    }

    public override float GetATK()
    {
        return this.atk + 5 * atkLevel; ;
    }
    public override float GetDEF()
    {
        return this.def + 5 * defLevel;
    }
    public override float GetFOC()
    {
        return this.foc + 1 * focLevel;
    }
    public override float GetAGI()
    {
        return this.agi + 1 * agiLevel;
    }
    public override float GetSpeed()
    {
        return this.speed;
    }

    public float GetAerusUpCost(Progress type)
    {
        switch (type)
        {
            case Progress.MaxHP:
                return this.aerusUpCost * (maxHPLevel - 1);
            case Progress.MaxMana:
                return this.aerusUpCost * (maxManaLevel - 1);
            case Progress.ATK:
                return this.aerusUpCost * (atkLevel - 1);
            case Progress.DEF:
                return this.aerusUpCost * (defLevel - 1);
            case Progress.FOC:
                return this.aerusUpCost * (focLevel - 1);
            case Progress.AGI:
                return this.aerusUpCost * (agiLevel - 1);
        }
        return -1;
    }

    public float GetExpUpCost(Progress type)
    {
        switch (type)
        {
            case Progress.MaxHP:
                return this.expUpCost * (maxHPLevel - 1);
            case Progress.MaxMana:
                return this.expUpCost * (maxManaLevel - 1);
            case Progress.ATK:
                return this.expUpCost * (atkLevel - 1);
            case Progress.DEF:
                return this.expUpCost * (defLevel - 1);
            case Progress.FOC:
                return this.expUpCost * (focLevel - 1);
            case Progress.AGI:
                return this.expUpCost * (agiLevel - 1);
        }
        return -1;
    }


    public Player CreateAsset(string name)
    {
        Player asset = ScriptableObject.CreateInstance<Player>();

        string assetName = "Assets/Prefabs/" + name + ".asset";

        AssetDatabase.CreateAsset(asset, assetName);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
        return asset;
    }

    public Player Clone()
    {
        Player newPlayer = (Player)this.MemberwiseClone();
        newPlayer.id = "player" + UnityEngine.Random.Range(1, 99999).ToString();
        return newPlayer;
    }

    public Player CloneForStage()
    {
        Player newPlayer = Clone();
        newPlayer.aerus = 0;
        newPlayer.exp = 0;
        newPlayer.venetia = 0;
        newPlayer.regenTimer = 0;
        return newPlayer;
    }

    public void LoadData(PlayerData playerData)
    {
        this.aerus = playerData.aerus;
        this.exp = playerData.exp;
        this.venetia = playerData.venetia;

        this.maxHPLevel = playerData.maxHPLevel;
        this.maxManaLevel = playerData.maxManaLevel;
        this.atkLevel = playerData.atkLevel;
        this.defLevel = playerData.defLevel;
        this.agiLevel = playerData.agiLevel;
        this.focLevel = playerData.focLevel;

        this.story = playerData.story;
        this.fireSkill = playerData.fireSkill;
        this.earthSkill = playerData.earthSkill;
        this.waterSkill = playerData.waterSkill;
        this.airSkill = playerData.airSkill;
    }

    public override void Upgrade(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.HP:
                this.maxHp += value;
                this.hp = this.maxHp;
                break;
            case Stat.Mana:
                this.maxMana += value;
                this.mana = this.maxMana;
                break;
            case Stat.ATK:
                this.atk += value;
                break;
            case Stat.DEF:
                this.def += value;
                break;
            case Stat.FOC:
                this.foc += value;
                break;
            case Stat.AGI:
                this.agi += value;
                break;
            case Stat.Shield:
                this.maxShield = value;
                this.shield = value;
                break;
        }
    }

    public override void Downgrade(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.HP:
                if (this.maxHp - value <= 0)
                {
                    this.maxHp = 0;
                }
                else
                {
                    this.maxHp -= value;
                }
                this.hp = this.maxHp;
                break;

            case Stat.Mana:
                if (this.maxMana - value <= 0)
                {
                    this.maxMana = 0;
                }
                else
                {
                    this.maxMana -= value;
                }
                this.mana = this.maxMana;
                break;

            case Stat.ATK:
                if (this.atk - value <= 0)
                {
                    this.atk = 1;
                }
                else
                {
                    this.atk -= value;
                }
                break;

            case Stat.DEF:
                if (this.def - value <= 0)
                {
                    this.def = 0;
                }
                else
                {
                    this.def -= value;
                }
                break;

            case Stat.FOC:
                if (this.foc - value <= 0)
                {
                    this.foc = 0;
                }
                else
                {
                    this.foc -= value;
                }
                break;

            case Stat.AGI:
                if (this.agi - value <= 0)
                {
                    this.agi = 0;
                }
                else
                {
                    this.agi -= value;
                }
                break;
        }
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
            case Stat.Mana:
                if (this.mana + value > this.maxMana)
                {
                    this.mana = this.maxMana;
                }
                else
                {
                    this.mana += value;
                }
                break;
        }
    }

    public void Collect(RewardType type, float value)
    {
        switch (type)
        {
            case RewardType.Aerus:
                this.aerus += value;
                break;
            case RewardType.ExpOrb:
                this.exp += value;
                break;
            case RewardType.Venetia:
                this.venetia += value;
                break;
        }
    }

    public void Pay(CostType type, float value)
    {
        switch (type)
        {
            case CostType.Mana:
                if (this.mana - value <= 0)
                {
                    this.mana = 0;
                }
                else
                {
                    this.mana -= value;

                }
                break;
            case CostType.Hp:
                if (this.hp - value <= 0)
                {
                    this.hp = 0;
                }
                else
                {
                    this.hp -= value;

                }
                break;
            case CostType.Aerus:
                if (this.aerus - value <= 0)
                {
                    this.aerus = 0;
                }
                else
                {
                    this.aerus -= value;

                }
                break;
            case CostType.Exp:
                if (this.exp - value <= 0)
                {
                    this.exp = 0;
                }
                else
                {
                    this.exp -= value;

                }
                break;
        }
    }

    public void Regenerating()
    {
        regenTimer += Time.deltaTime;
        if (regenTimer >= 1)
        {
            Heal(Stat.Mana, manaRegen);
            Heal(Stat.HP, hpRegen);
            regenTimer = 0;
        }
    }





}
