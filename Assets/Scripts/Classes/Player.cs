using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Player : Character
{
    public float maxMana;

    [HideInInspector]
    public float mana, maxShield, shield, aerus, exp, level;


    private void OnEnable()
    {
        // Kode yang ingin dijalankan saat scriptable object diaktifkan pertama kali
        this.id = "player" + UnityEngine.Random.Range(1, 99999).ToString();
        this.hp = this.maxHp;
        this.mana = this.maxMana;
        this.maxShield = 0;
        this.shield = maxShield;
        this.aerus = 0;
        this.level = 1;
    }

    public float movementSpeed
    {
        get
        {
            // Isi dengan logika atau nilai yang ingin Anda kembalikan
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

    public Player CloneObject()
    {
        Player newPlayer = ScriptableObject.CreateInstance<Player>();
        newPlayer.maxHp = this.maxHp;
        newPlayer.hp = this.hp;
        newPlayer.atk = this.atk;
        newPlayer.def = this.def;
        newPlayer.agi = this.agi;
        newPlayer.speed = this.speed;
        newPlayer.foc = this.foc;
        newPlayer.maxMana = this.maxMana;
        newPlayer.mana = this.mana;
        newPlayer.maxShield = this.maxShield;
        newPlayer.shield = this.shield;
        newPlayer.aerus = this.aerus;
        newPlayer.exp = this.exp;
        newPlayer.level = this.level;
        return newPlayer;
    }





}
