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

    private BuffSystem buffSystem;

    // private AttackAttribute characterAttr;

    void Start()
    {
        buffSystem = GetComponent<BuffSystem>();
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
        switch (type)
        {
            case CharacterType.Player:
                Player playerDefender = (Player)defender;

                // kalau sedang menggunakan invitro
                if (buffSystem.buffsActive.FindIndex(buff => buff.id == "skill_invitro") != -1)
                {
                    // print("Invitro sedang aktif");
                    float gainHp = 0.5f * totalDamage;

                    // kalau setelah ditambahkan, > max HP,
                    // jadikan banyak hp = maxHP
                    if (playerDefender.hp + gainHp >= playerDefender.maxHp)
                    {
                        playerDefender.hp = playerDefender.maxHp;
                    }
                    // kalau tidak, tambahkan hp
                    else
                    {
                        playerDefender.hp += gainHp;

                    }
                }

                if (playerDefender.shield > 0)
                {
                    // kalau ternyata damage yg diterima > shield yg ada, 
                    // berarti masih ada damage yg harus diterima hp
                    if (totalDamage > playerDefender.shield)
                    {
                        totalDamage -= playerDefender.shield;
                        playerDefender.shield = 0;
                    }
                    // kalau ternyata shield yg ada > damage yg diterima,
                    // berarti hp tidak perlu menerima damage lagi
                    else
                    {
                        playerDefender.shield -= totalDamage;
                        return;
                    }
                }

                break;
        }

        finalDamage = totalDamage - def * 0.5f;
        if (finalDamage <= 1)
        {
            finalDamage = 1;
        }
        defender.hp -= finalDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (type)
        {
            // saat enemy sebagai defender
            case CharacterType.Enemy:
                if (other.CompareTag("Damage"))
                {
                    Skill skill = other.GetComponent<SkillController>().skill;
                    // jika enemy sedang diserang skill yang hit sekali
                    if (skill.HitType != SkillHitType.Once)
                    {
                        return;
                    }

                    // jika enemy tipe terbang dan skill yang kena bukan angin,
                    // maka enemy tidak akan menerima damage
                    if (((Enemy)defender).type == EnemyType.Flying)
                    {
                        if (skill.Element != Element.Air)
                        {
                            return;
                        }
                    }
                    gameObject.GetComponent<MobController>().Damaged();
                    TakeDamage(other.GetComponent<AttackSystem>().DealDamage());

                }
                break;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (type)
        {
            // saat player sebagai defender
            case CharacterType.Player:
                // enemy sedang menyerang player
                if (other.CompareTag("Enemy"))
                {
                    // kalau player terkena bayangan musuh terbang,
                    // maka player tidak akan terkena damage
                    if (other.GetComponent<MobController>().enemy.type == EnemyType.Flying)
                    {
                        return;
                    }

                    float dealDamage = other.GetComponent<AttackSystem>().DealDamage();

                    // jika player punya buff thorn, pantulkan damage ke musuh yg serang
                    if (buffSystem.CheckBuff(BuffType.Thorn))
                    {
                        float thornDamage = buffSystem.buffsActive.Find(buff => buff.type == BuffType.Thorn).value;

                        // musuh yg menyerang juga terkena damage
                        other.GetComponent<MobController>().Damaged();
                        other.GetComponent<DefenseSystem>().Attacked(thornDamage);
                    }

                    gameObject.GetComponent<PlayerController>().Damaged();
                    Attacked(dealDamage);
                }
                break;

            // saat enemy sebagai defender
            case CharacterType.Enemy:

                // player sedang menyerang enemy
                if (other.CompareTag("Damage"))
                {
                    Skill skill = other.GetComponent<SkillController>().skill;

                    // jika enemy sedang diserang skill yg hit nya ber waktu
                    if (skill.HitType != SkillHitType.Temporary)
                    {
                        return;
                    }

                    // jika enemy tipe terbang dan skill yang kena bukan angin,
                    // maka enemy tidak akan menerima damage
                    if (((Enemy)defender).type == EnemyType.Flying)
                    {
                        if (skill.Element != Element.Air)
                        {
                            return;
                        }
                    }

                    gameObject.GetComponent<MobController>().Damaged();
                    Attacked(other.GetComponent<AttackSystem>().DealDamage());

                }
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (type)
        {
            case CharacterType.Enemy:
                if (other.CompareTag("Damage"))
                {
                    timer = 0;

                    gameObject.GetComponent<MobController>().Undamaged();

                }
                break;
            case CharacterType.Player:
                if (other.CompareTag("Enemy"))
                {
                    timer = 0;
                    gameObject.GetComponent<PlayerController>().Undamaged();

                }
                break;
        }

    }

    private void Attacked(float totalDamage)
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

    // private bool FlyingEnemyCanAttackedBy(Skill skill)
    // {

    //     // return ((Enemy)defender).type == EnemyType.Flying && skill.Element == Element.Air;

    // }

}