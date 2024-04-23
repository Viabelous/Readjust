using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// dikasih ke skill
public class DefenseSystem : MonoBehaviour
{

    public CharacterType type;

    [HideInInspector]
    private float finalDamage, def;
    // private AttackSystem attacker;
    private Character defender;
    private bool isInstantiate = true;

    private float maxTimer = 1, timer = 0;

    // private AttackAttribute characterAttr;

    void Start()
    {
        // switch (type)
        // {
        //     case CharacterType.Player:
        //         character = GetComponent<PlayerController>().player;
        //         break;
        //     case CharacterType.Enemy:
        //         character = GetComponent<MobController>().enemy;
        //         break;
        // }
        // def = character.def;
    }

    void Update()
    {
        if (isInstantiate)
        {
            switch (type)
            {
                case CharacterType.Player:
                    defender = GetComponent<PlayerController>().player;
                    break;
                case CharacterType.Enemy:
                    defender = GetComponent<MobController>().enemy;
                    break;
            }
            def = defender.def;
            isInstantiate = false;
        }

    }

    public void TakeDamage(float totalDamage)
    {
        finalDamage = totalDamage - def * 0.5f;
        if (finalDamage <= 1)
        {
            finalDamage = 1;
        }
        defender.hp -= finalDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (type == CharacterType.Enemy && other.CompareTag("Damage"))
        {
            Skill skill = other.GetComponent<SkillController>().skill;
            if (skill.hitType == SkillHitType.Once)
            {
                TakeDamage(other.GetComponent<AttackSystem>().DealDamage());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (type)
        {
            case CharacterType.Player:
                if (other.CompareTag("Enemy"))
                {
                    Attacking(other.GetComponent<AttackSystem>().DealDamage());
                }
                break;

            case CharacterType.Enemy:
                if (other.CompareTag("Damage"))
                {
                    Skill skill = other.GetComponent<SkillController>().skill;
                    if (skill.hitType == SkillHitType.Temporary)
                    {
                        Attacking(other.GetComponent<AttackSystem>().DealDamage());
                    }
                }
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(type == CharacterType.Player ? "Enemy" : "Damage"))
        {
            timer = 0;
        }
    }

    private void Attacking(float totalDamage)
    {
        if (timer <= 0)
        {
            timer = maxTimer;
            TakeDamage(totalDamage);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

}