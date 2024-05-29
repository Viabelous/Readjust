using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public enum HistoryType
{
    Score,
    Time
}

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

    [Header("Score")]
    // 0 -> score, 1 -> time
    private Dictionary<DateTime, List<float>> scores = new Dictionary<DateTime, List<float>>();



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
                this.maxHPLevel += value;
                break;

            case Progress.MaxMana:
                this.maxManaLevel += value;
                break;

            case Progress.ATK:
                this.atkLevel += value;
                break;

            case Progress.DEF:
                this.defLevel += value;
                break;

            case Progress.FOC:
                this.focLevel += value;
                break;

            case Progress.AGI:
                this.agiLevel += value;
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
                return this.aerusUpCost * maxHPLevel;
            case Progress.MaxMana:
                return this.aerusUpCost * maxManaLevel;
            case Progress.ATK:
                return this.aerusUpCost * atkLevel;
            case Progress.DEF:
                return this.aerusUpCost * defLevel;
            case Progress.FOC:
                return this.aerusUpCost * focLevel;
            case Progress.AGI:
                return this.aerusUpCost * agiLevel;
        }
        return -1;
    }

    public float GetExpUpCost(Progress type)
    {
        switch (type)
        {
            case Progress.MaxHP:
                return this.expUpCost * maxHPLevel;
            case Progress.MaxMana:
                return this.expUpCost * maxManaLevel;
            case Progress.ATK:
                return this.expUpCost * atkLevel;
            case Progress.DEF:
                return this.expUpCost * defLevel;
            case Progress.FOC:
                return this.expUpCost * focLevel;
            case Progress.AGI:
                return this.expUpCost * agiLevel;
        }
        return -1;
    }

    public Dictionary<DateTime, List<float>> GetScores()
    {
        return this.scores;
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
        newPlayer.hp = GetMaxHP();
        newPlayer.mana = GetMaxMana();
        newPlayer.maxShield = 0;
        newPlayer.shield = maxShield;
        newPlayer.aerus = 0;
        newPlayer.exp = 0;
        newPlayer.venetia = 0;
        newPlayer.regenTimer = 0;
        return newPlayer;
    }

    public Dictionary<string, object> DataToJson()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("aerus", this.aerus);
        data.Add("exp", this.exp);
        data.Add("venetia", this.venetia);
        data.Add("maxHPLevel", this.maxHPLevel);
        data.Add("maxManaLevel", this.maxManaLevel);
        data.Add("atkLevel", this.atkLevel);
        data.Add("defLevel", this.defLevel);
        data.Add("focLevel", this.focLevel);
        data.Add("agiLevel", this.agiLevel);

        data.Add("story", this.story);
        data.Add("fireSkill", this.fireSkill);
        data.Add("earthSkill", this.earthSkill);
        data.Add("waterSkill", this.waterSkill);
        data.Add("airSkill", this.airSkill);

        data.Add("scores", this.scores);

        return data;
    }

    public void JsonToPlayer(Dictionary<string, object> data)
    {
        this.aerus = float.Parse(data["aerus"].ToString());
        this.exp = float.Parse(data["exp"].ToString());
        this.venetia = float.Parse(data["venetia"].ToString());
        this.maxHPLevel = int.Parse(data["maxHPLevel"].ToString());
        this.maxManaLevel = int.Parse(data["maxManaLevel"].ToString());
        this.atkLevel = int.Parse(data["atkLevel"].ToString());
        this.defLevel = int.Parse(data["defLevel"].ToString());
        this.focLevel = int.Parse(data["focLevel"].ToString());
        this.agiLevel = int.Parse(data["agiLevel"].ToString());
        this.story = int.Parse(data["story"].ToString());
        this.fireSkill = int.Parse(data["fireSkill"].ToString());
        this.earthSkill = int.Parse(data["earthSkill"].ToString());
        this.waterSkill = int.Parse(data["waterSkill"].ToString());
        this.airSkill = int.Parse(data["airSkill"].ToString());

        foreach (var score in data["scores"] as Dictionary<DateTime, List<float>>)
        {
            if (this.scores.ContainsKey(score.Key))
            {
                continue;
            }
            this.scores.Add(score.Key, score.Value);
        }

    }

    // public void LoadData(PlayerData playerData)
    // {
    //     this.aerus = playerData.aerus;
    //     this.exp = playerData.exp;
    //     this.venetia = playerData.venetia;

    //     this.maxHPLevel = playerData.maxHPLevel;
    //     this.maxManaLevel = playerData.maxManaLevel;
    //     this.atkLevel = playerData.atkLevel;
    //     this.defLevel = playerData.defLevel;
    //     this.agiLevel = playerData.agiLevel;
    //     this.focLevel = playerData.focLevel;

    //     this.story = playerData.story;
    //     this.fireSkill = playerData.fireSkill;
    //     this.earthSkill = playerData.earthSkill;
    //     this.waterSkill = playerData.waterSkill;
    //     this.airSkill = playerData.airSkill;
    // }

    public override void Upgrade(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.HP:
                this.maxHp += value;
                this.hp = GetMaxHP();
                break;
            case Stat.Mana:
                this.maxMana += value;
                this.mana = GetMaxMana();
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
                if (this.hp + value > GetMaxHP())
                {
                    this.hp = GetMaxHP();
                }
                else
                {
                    this.hp += value;
                }
                break;
            case Stat.Mana:
                if (this.mana + value > GetMaxMana())
                {
                    this.mana = GetMaxMana();
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

    public void SaveHistory(float score, float time)
    {
        List<float> scores = new List<float>() { score, time };
        this.scores.Add(DateTime.Now, scores);
    }
}
