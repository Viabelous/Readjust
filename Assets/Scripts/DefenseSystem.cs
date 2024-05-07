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
    private GameObject player;


    // private AttackAttribute characterAttr;

    void Start()
    {
        buffSystem = GetComponent<BuffSystem>();
        player = GameObject.Find("Player");
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
                case CharacterType.FlyingEnemy:
                    defender = transform.parent.GetComponent<MobController>().enemy;
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

        if (type == CharacterType.Enemy || type == CharacterType.FlyingEnemy)
        {
            print("HP musuh: " + defender.hp);
        }
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

                    // damage hanya akan diberikan jika skill hanya memberikan damage sekali
                    if (EnemyDefendingIsValid(skill, SkillHitType.Once))
                    {

                        float dealDamage = other.GetComponent<AttackSystem>().DealDamage();
                        TakeDamage(dealDamage);


                        // kalau player punya buff nexus
                        AttackIfNexusActivated(skill, dealDamage);
                    }

                }
                break;

            case CharacterType.FlyingEnemy:
                if (other.CompareTag("Damage"))
                {
                    Skill skill = other.GetComponent<SkillController>().skill;

                    // jika bukan elemen angin dan bukan me-lock musuh, maka tidak perlu kasih damage
                    if (skill.Element != Element.Air && skill.MovementType != SkillMovementType.Locking)
                    {
                        return;
                    }

                    // kalau bukan yg di-lock, ga ush kasih damage
                    if (skill.LockedEnemy.parent.GetComponent<MobController>().enemy.id != defender.id)
                    {
                        return;
                    }

                    float dealDamage = other.GetComponent<AttackSystem>().DealDamage();
                    transform.parent.GetComponent<MobController>().Damaged();
                    TakeDamage(dealDamage);

                    // kalau player punya buff nexus
                    AttackIfNexusActivated(skill, dealDamage);
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

                    // damage hanya akan diberikan jika skill merupakan skill ber waktu
                    if (EnemyDefendingIsValid(skill, SkillHitType.Temporary))
                    {
                        Attacked(other.GetComponent<AttackSystem>().DealDamage());
                    }

                }
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (type)
        {
            case CharacterType.Player:
                if (other.CompareTag("Enemy"))
                {
                    timer = 0;
                    gameObject.GetComponent<PlayerController>().Undamaged();

                }
                break;

            case CharacterType.Enemy:
                if (other.CompareTag("Damage"))
                {
                    timer = 0;
                    gameObject.GetComponent<MobController>().Undamaged();

                }
                break;

            case CharacterType.FlyingEnemy:
                if (other.CompareTag("Damage"))
                {
                    timer = 0;
                    transform.parent.GetComponent<MobController>().Undamaged();

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

    private bool EnemyDefendingIsValid(Skill skill, SkillHitType damageType)
    {
        if (skill.HitType != damageType)
        {
            return false;
        }

        Enemy enemyDefender = (Enemy)defender;

        // kalau bayangan terkena damage dari skill yang tipenya lock,
        // maka musuh terbang tidak akan terkena damage.
        // ceritanya kena skill lock yg lagi lewat di bayangannya :)
        if (enemyDefender.type == EnemyType.Flying && skill.MovementType == SkillMovementType.Locking)
        {
            return false;
        }

        // jika enemy tipe terbang dan skill yang kena bukan angin,
        // maka enemy tidak akan menerima damage
        if (enemyDefender.type == EnemyType.Flying)
        {
            if (skill.Element != Element.Air)
            {
                return false;
            }
        }

        gameObject.GetComponent<MobController>().Damaged();
        print("MERAHHH");
        return true;
    }


    private void AttackIfNexusActivated(Skill skill, float dealDamage)
    {
        // kalau player punya buff nexus
        if (player.GetComponent<BuffSystem>().CheckBuff(BuffType.Nexus))
        {
            StartCoroutine(DamagedByNexus(skill, dealDamage));
        }

    }

    private IEnumerator DamagedByNexus(Skill skill, float dealDamage)
    {
        skill.LockedEnemy.GetComponent<MobController>().Damaged();
        skill.LockedEnemy.GetComponent<DefenseSystem>().TakeDamage(0.3f * dealDamage);

        yield return new WaitForSeconds(0.2f);
        skill.LockedEnemy.GetComponent<MobController>().Undamaged();
    }


}