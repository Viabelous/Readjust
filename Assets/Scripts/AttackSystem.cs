using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// dikasih ke skill
public class AttackSystem : MonoBehaviour
{

    public CharacterType type;
    // public GameObject characterObj;

    [HideInInspector]

    private float totalDamage, damage;
    private bool isInstantiate = true;
    private Character character;

    void Start()
    {
        // switch (type)
        // {
        //     case CharacterType.player:
        //         character = GameObject.FindObjectOfType<PlayerController>().player;
        //         damage = GetComponent<SkillSetting>().skill.damage;
        //         break;
        //     case CharacterType.enemy:
        //         character = GetComponent<MobController>().enemy;
        //         damage = character.atk;
        //         break;
        // }
    }

    void Update()
    {
        if (isInstantiate)
        {
            switch (type)
            {
                case CharacterType.player:
                    character = GameObject.FindObjectOfType<PlayerController>().player;
                    damage = GetComponent<SkillSetting>().skill.damage;
                    break;
                case CharacterType.enemy:
                    character = GetComponent<MobController>().enemy;
                    damage = character.atk;
                    break;
            }
            isInstantiate = false;
        }
    }

    public float DealDamage()
    {
        // damage dari skill + efek skill
        totalDamage = damage + character.atk * character.foc * 0.01f;
        return totalDamage;
    }

}