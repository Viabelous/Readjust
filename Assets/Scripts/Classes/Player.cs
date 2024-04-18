using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public float maxHp;
    public float maxMana;
    public float maxShield;
    public float aeurus;
    public float level;

    // public List<Skill> selectedSkills;

    public Player(float maxHp, float maxMana, float maxShield, float aeurus, float level)
    {
        this.maxHp = maxHp;
        this.maxMana = maxMana;
        this.maxShield = maxShield;
        this.aeurus = aeurus;
        this.level = level;

        // selectedSkills = new List<Skill>() { GameManager.skills[2], GameManager.skills[0], GameManager.skills[3], GameManager.skills[1] };

    }

}
