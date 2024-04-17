using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public float maxHp = 100;
    public float maxMana = 50;
    public float maxShield = 30;

    public List<Skill> selectedSkills;

    public Player()
    {
        selectedSkills = new List<Skill> { GameManager.skills[2], GameManager.skills[0], GameManager.skills[3], GameManager.skills[1] };

    }

}
