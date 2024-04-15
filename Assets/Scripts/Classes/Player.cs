using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public float hp = 100;
    public float mana = 50;
    public float defense = 30;

    public Skill[] selectedSkills = new Skill[7];

    // public Skill[] selectedSkills = {
    //     GameManager.skillsAvailable[0],
    //     GameManager.skillsAvailable[1],
    //     GameManager.skillsAvailable[2],
    //     GameManager.skillsAvailable[3],
    // };

    public Player()
    {
        selectedSkills[0] = GameManager.skillsAvailable[0];
        selectedSkills[1] = GameManager.skillsAvailable[1];
        selectedSkills[2] = GameManager.skillsAvailable[2];
        selectedSkills[3] = GameManager.skillsAvailable[3];
    }

}
