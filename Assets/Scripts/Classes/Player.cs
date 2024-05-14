using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu]
public class Player : Character
{
    public float maxMana;

    [ReadOnly]
    public float mana, maxShield, shield, aerus, exp, venetia, story;


    private void OnEnable()
    {
        // Kode yang ingin dijalankan saat scriptable object diaktifkan pertama kali
        this.id = "player" + UnityEngine.Random.Range(1, 99999).ToString();
        this.hp = this.maxHp;
        this.mana = this.maxMana;
        this.maxShield = 0;
        this.shield = maxShield;
        this.aerus = 0;
        this.exp = 0;
        this.venetia = 0;
        this.story = 0;
    }

    public float MovementSpeed
    {
        get
        {
            return speed + agi * 0.1f;
        }
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
                this.maxHp -= value;
                this.hp = this.maxHp;
                break;
            case Stat.Mana:
                this.maxMana -= value;
                this.mana = this.maxMana;
                break;
            case Stat.ATK:
                this.atk -= value;
                break;
            case Stat.DEF:
                this.def -= value;
                break;
            case Stat.FOC:
                this.foc -= value;
                break;
            case Stat.AGI:
                this.agi -= value;
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
                mana -= value;
                break;
            case CostType.Hp:
                hp -= value;
                break;
            case CostType.Aerus:
                aerus -= value;
                break;
            case CostType.Exp:
                exp -= value;
                break;
        }
    }



    // public Player CloneObject()
    // {
    //     Player newPlayer = ScriptableObject.CreateInstance<Player>();
    //     newPlayer.maxHp = this.maxHp;
    //     newPlayer.hp = this.hp;
    //     newPlayer.atk = this.atk;
    //     newPlayer.def = this.def;
    //     newPlayer.agi = this.agi;
    //     newPlayer.speed = this.speed;
    //     newPlayer.foc = this.foc;
    //     newPlayer.maxMana = this.maxMana;
    //     newPlayer.mana = this.mana;
    //     newPlayer.maxShield = this.maxShield;
    //     newPlayer.shield = this.shield;
    //     newPlayer.aerus = this.aerus;
    //     newPlayer.exp = this.exp;
    //     newPlayer.story = this.story;
    //     return newPlayer;
    // }





}
