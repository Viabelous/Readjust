using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public float hp = 100;
    public float mana = 50;
    public float defense = 30;

    // public Skill[] usedSkills = new Skill[7];
    public Skill[] usedSkills = {
        new Skill("ignite"),
        new Skill("waterwall"),
        new Skill("high_tide")
    };

}
