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
    private float finalDamage;
    // private AttackSystem attacker;
    private Character defender;
    private bool isInstantiate = true;

    private float maxTimer = 1, timer = 0;

    private BuffSystem buffSystem;
    private GameObject player;


    // private AttackAttribute characterAttr;

    void Start()
    {
        player = GameObject.Find("Player");
        if (type == CharacterType.Player)
        {
            buffSystem = GetComponent<BuffSystem>();
        }
        else
        {
            buffSystem = player.GetComponent<BuffSystem>();
        }
    }

    void Update()
    {
        if (isInstantiate)
        {
            SetDefender();

            isInstantiate = false;
        }

    }

    public void TakeDamage(float totalDamage)
    {
        if (defender == null)
        {
            SetDefender();
        }

        switch (type)
        {
            case CharacterType.Player:
                Player playerDefender = (Player)defender;

                // kalau ada skill invitro, healing player ketika kena damage
                HealingByInvitroSkill(playerDefender, totalDamage);

                if (playerDefender.shield > 0)
                {

                    // kalau ternyata damage yg diterima > shield yg ada, 
                    // berarti masih ada damage yg harus diterima hp
                    if (totalDamage > playerDefender.shield)
                    {
                        totalDamage -= playerDefender.shield;
                        playerDefender.shield = 0;

                        // hapus efek healing & sheild invitro karena shield sudah habis
                        GameObject invitro = GameObject.Find("Invitro(Clone)");
                        if (invitro != null)
                        {
                            Destroy(invitro);
                        }

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

        finalDamage = totalDamage - defender.GetDEF() * 0.5f;

        if (finalDamage <= 1)
        {
            finalDamage = 1;
        }

        // print("totalDamage = " + totalDamage);
        // print("finalDamage = " + finalDamage);

        defender.hp -= finalDamage;

        if (type == CharacterType.Enemy || type == CharacterType.FlyingEnemy)
        {
            //print("Enemy HP: " + defender.GetHP());
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
                        AttackIfNexusActivated(dealDamage);
                    }

                }
                break;

            case CharacterType.FlyingEnemy:
                if (other.CompareTag("Damage"))
                {
                    Skill skill = other.GetComponent<SkillController>().skill;

                    if (FlyingEnemyDefendingIsValid(skill, SkillHitType.Once))
                    {
                        float dealDamage = other.GetComponent<AttackSystem>().DealDamage();
                        TakeDamage(dealDamage);

                        // kalau player punya buff nexus
                        AttackIfNexusActivated(dealDamage);

                    }

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
                        other.GetComponent<DefenseSystem>().Attacked(thornDamage);
                        other.GetComponent<MobController>().Effected("thorn");
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
                        float dealDamage = other.GetComponent<AttackSystem>().DealDamage();
                        Attacked(dealDamage);

                        // kalau player punya buff nexus
                        AttackIfNexusActivated(dealDamage);
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

        if (skill.MovementType == SkillMovementType.Locking)
        {
            // kalau bayangan terkena damage dari skill yang tipenya lock,
            // maka musuh terbang tidak akan terkena damage.
            // ceritanya kena skill lock yg lagi lewat di bayangannya :)
            if (enemyDefender.type == EnemyType.Flying)
            {
                return false;
            }

            // kalau kena damage yg tipenya lock,
            // tapi bukan yg di-lock
            if (skill.LockedEnemy != transform)
            {
                return false;
            }
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
        return true;
    }


    private bool FlyingEnemyDefendingIsValid(Skill skill, SkillHitType damageType)
    {
        if (skill.HitType != damageType)
        {
            return false;
        }

        if (skill.Element != Element.Air)
        {
            return false;
        }

        // jika bukan elemen angin dan bukan me-lock musuh, maka tidak perlu kasih damage
        // misalnya skill angin yang tipe area (typhoon, whirlwind dkk)
        if (skill.MovementType != SkillMovementType.Locking)
        {
            return false;
        }

        // kalau skill me-lock musuh, tapi yang di lock bukan orang ini
        // artinya skillnya cuma lewati orang ini
        if (skill.MovementType == SkillMovementType.Locking && skill.LockedEnemy != transform)
        {
            return false;
        }

        // // kalau bukan yg di-lock, ga ush kasih damage
        // if (skill.LockedEnemy.parent.GetComponent<MobController>().enemy.id != defender.id)
        // {
        //     return false;
        // }
        StartCoroutine(FlyingEnemyDamaged());


        return true;
    }

    private IEnumerator FlyingEnemyDamaged()
    {
        transform.parent.GetComponent<MobController>().Damaged();
        yield return new WaitForSeconds(0.2f);
        transform.parent.GetComponent<MobController>().Undamaged();
    }

    private void AttackIfNexusActivated(float dealDamage)
    {
        // kalau player punya buff nexus
        if (player.GetComponent<BuffSystem>().CheckBuff("Nexus"))
        {
            DamagedByNexusSkill(dealDamage);
        }

    }

    private void DamagedByNexusSkill(float dealDamage)
    {
        Skill nexus = GameObject.FindObjectOfType<NexusBehaviour>().GetComponent<SkillController>().skill;
        Transform lockedEnemy = nexus.LockedEnemy;

        if (lockedEnemy == null)
        {
            return;
        }

        MobController mobController = lockedEnemy.GetComponent<MobController>();
        mobController.Effected("nexus");
        Instantiate(((Nexus)nexus).DamageEffect, mobController.transform.position, Quaternion.identity);

        float nexusDamage = ((Nexus)nexus).dmgPersenOfTotalDmgFinal * dealDamage;

        // kalau damage yg diterima merupakan damage dari breezewheel,
        // damage ke target nexus akan berkurang setengah
        if (buffSystem.CheckBuff(BuffType.Breezewheel))
        {
            nexusDamage = nexusDamage * 0.5f;
        }

        // berikan damage ke musuh yg ditandai
        lockedEnemy.GetComponent<DefenseSystem>().TakeDamage(nexusDamage);

    }

    private void HealingByInvitroSkill(Player playerDefender, float takenDamage)
    {
        GameObject invitro = GameObject.Find("Invitro(Clone)");

        // kalau sedang menggunakan invitro
        if (buffSystem.CheckBuff("Invitro") && invitro != null)
        {
            print("Heal ~");
            Skill skill = invitro.GetComponent<SkillController>().skill;
            float gainHp = ((Invitro)skill).hpPersenOfDmg * takenDamage;
            playerDefender.Heal(Stat.HP, gainHp);

            // // kalau setelah ditambahkan, > max HP,
            // // jadikan banyak hp = maxHP
            // if (playerDefender.hp + gainHp >= playerDefender.maxHp)
            // {
            //     playerDefender.hp = playerDefender.maxHp;
            // }
            // // kalau tidak, tambahkan hp
            // else
            // {
            //     playerDefender.hp += gainHp;
            // }
        }
    }


    private void SetDefender()
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
    }


}