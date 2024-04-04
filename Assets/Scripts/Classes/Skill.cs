using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill
{
    public string name;
    public Animator animator;
    public GameObject skillObject;

    public Skill(string name)
    {
        this.name = name;
        this.skillObject = GameObject.Find(name);
    }

    public void Attack()
    {
        switch (name)
        {
            case "ignite":
                skillObject.GetComponent<IgniteSkill>().Active();
                break;
            case "waterwall":
                skillObject.GetComponent<WaterwallSkill>().Active();
                break;
            case "high_tide":
                skillObject.GetComponent<HighTideSkill>().Active();
                break;
            case "whirlwind":
                skillObject.GetComponent<WhirlwindSkill>().Active();
                break;
        }
    }
}