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
    private Character character;
    private bool isInstantiate = true;

    private float maxTimer = 1, timer = 0;

    // private AttackAttribute characterAttr;

    void Start()
    {
        // switch (type)
        // {
        //     case CharacterType.player:
        //         character = GetComponent<PlayerController>().player;
        //         break;
        //     case CharacterType.enemy:
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
                case CharacterType.player:
                    character = GetComponent<PlayerController>().player;
                    break;
                case CharacterType.enemy:
                    character = GetComponent<MobController>().enemy;
                    break;
            }
            def = character.def;
            isInstantiate = false;
        }

    }

    public void TakeDamage(float totalDamage)
    {
        finalDamage = totalDamage - def * 0.5f;
        if(finalDamage <= 1){
            finalDamage = 1;
        }
        character.hp -= finalDamage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(type == CharacterType.player ? "Enemy" : "Damage"))
        {
            if (timer <= 0)
            {
                timer = maxTimer;
                TakeDamage(other.GetComponent<AttackSystem>().DealDamage());
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

}