using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    public float maxMana, mana;
    public float maxShield, shield;
    public float aerus;
    public float exp;
    public float level;


    public Player(float maxHp, float maxMana, float atk, float def, float agi, float foc)
    {
        this.maxHp = maxHp;
        this.hp = this.maxHp;
        this.atk = atk;
        this.def = def;
        this.agi = agi;
        this.foc = foc;
        this.maxMana = maxMana;
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
            return 4 + agi * 0.1f;

        }
    }





}
