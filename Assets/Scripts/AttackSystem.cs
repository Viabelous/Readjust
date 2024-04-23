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
    // private bool isInstantiate = true;
    private Character attacker;

    void Start()
    {

    }

    void Update()
    {

    }

    public float DealDamage()
    {
        // ditaruh di sini karena dipakenya di skill bukan di player yg mana skill munculnya sbenetar ae
        // kalo ditaruh di update kadang error, 
        // mungkin malah bisa jadi deal damage dipanggil duluan dari pada update (?)

        switch (type)
        {
            case CharacterType.player:
                attacker = GameObject.FindWithTag("Player").GetComponent<PlayerController>().player;
                damage = GetComponent<SkillController>().skill.damage;
                break;
            case CharacterType.enemy:
                attacker = GetComponent<MobController>().enemy;
                damage = attacker.atk;
                break;
        }

        if (UnityEngine.Random.Range(0f, 100f) <= attacker.foc && attacker.foc != 0)
        {
            totalDamage = damage * 2.5f;
        }
        else
        {
            totalDamage = damage;
        }

        return totalDamage;
    }

}