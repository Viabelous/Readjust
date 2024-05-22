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
    private BuffSystem buffSystem;

    void Start()
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
    }

    public float DealDamage()
    {
        // ditaruh di sini karena dipakenya di skill bukan di player yg mana skill munculnya sbenetar ae
        // kalo ditaruh di update kadang error, 
        // mungkin malah bisa jadi deal damage dipanggil duluan dari pada update (?)

        switch (type)
        {
            case CharacterType.Player:
                attacker = GameObject.FindWithTag("Player").GetComponent<PlayerController>().player;
                damage = GetComponent<SkillController>().skill.GetDamage((Player)attacker);
                break;

            case CharacterType.Enemy:
                attacker = GetComponent<MobController>().enemy;
                damage = attacker.GetATK();
                break;
        }

        if (UnityEngine.Random.Range(0f, 100f) <= attacker.GetFOC() && attacker.GetFOC() != 0)
        {
            totalDamage = damage * 2.5f;
            // totalDamage = damage;
        }
        else
        {
            totalDamage = damage;
        }

        totalDamage += totalDamage * DamageBooster();

        // kalau player pakai skill A Breeze Being Told, 
        // maka total damage akan bertambah sebanyak buff value persen dari total
        if (buffSystem.CheckBuff(BuffType.Breezewheel))
        {
            totalDamage += totalDamage * buffSystem.GetBuffValues(BuffType.Breezewheel);
        }

        return totalDamage;
    }

    // peningkatan damage dari item
    private float DamageBooster()
    {
        if (type != CharacterType.Player)
        {
            return 0;
        }

        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        BuffType buffType = BuffType.Custom;
        switch (GetComponent<SkillController>().skill.Element)
        {
            case Element.Fire:
                buffType = BuffType.Fire;
                break;
            case Element.Earth:
                buffType = BuffType.Earth;
                break;
            case Element.Water:
                buffType = BuffType.Water;
                break;
            case Element.Air:
                buffType = BuffType.Air;
                break;

        }
        float boosterDmg = buffSystem.GetAllBuffValues(buffType);
        return boosterDmg;
    }

}