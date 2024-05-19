using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Holy Sonata")]
public class HolySonata : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float HPManaPersenOfFOC;
    // [SerializeField] private float manaPersenOfFOC;

    [Header("Debuff Value")]
    [SerializeField] private float atkPersenOfATK;
    [SerializeField] private float defPersenOfDEF;

    private float healHPValue, healManaValue, harmonyTimer;
    private BuffSystem buffSystem;
    private DebuffSystem debuffSystem;
    private Animator animator;
    private Player player;
    private Buff buff, debuffATK, debuffDEF;

    public float HPManaPersenOfFOCFinal
    {
        get { return HPManaPersenOfFOC + 0.2f * (level - 1); }
    }

    public float atkPersenOfATKFinal
    {
        get { return atkPersenOfATK - 0.2f * (level - 1); }
    }
    public float defPersenOfDEFFinal
    {
        get { return defPersenOfDEF - 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Memberikan status {Harmony} pada karakter yang akan terus mengisi HP dan Mana karakter sebanyak " + HPManaPersenOfFOCFinal * 100 + "% FOC setiap detik namun menurunkan ATK serta DEF karakter masing-masing sebanyak " + atkPersenOfATKFinal * 100 + "% ATK dan " + defPersenOfDEFFinal * 100 + "% DEF. Menggunakan kembali skill ini saat sedang memiliki status {Harmony} tidak akan mengurangi Mana dan akan menonaktifkan status {Harmony}. Menggunakan skill ini akan menghapus status {Idiosyncrasy}.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        harmonyTimer = 0;
        animator = gameObject.GetComponent<Animator>();
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        debuffSystem = buffSystem.GetComponent<DebuffSystem>();

        player = buffSystem.GetComponent<PlayerController>().player;

        if (buffSystem.CheckBuff(BuffType.Harmony))
        {
            // tidak gunakan mana, tapi memakan cooldown
            StartCooldown();

            buffSystem.DeactivateBuff(BuffType.Harmony);
            Destroy(gameObject); // hapus gameobject yg sebagai trigger
        }
        else
        {
            if (buffSystem.CheckBuff(BuffType.Idiosyncrasy))
            {
                buffSystem.DeactivateAllRelatedBuff(
                    buffSystem.GetBuffNameOfType(BuffType.Idiosyncrasy)
                );
            }


            // gunakan mana, tapi tidak memakan cooldown
            PayWithCostType(buffSystem.GetComponent<PlayerController>().player);
            // Payment(buffSystem.transform);

            buff = new Buff(
                    this.id,
                    this.Name,
                    BuffType.Harmony,
                    0,
                    Timer
                );
            buffSystem.ActivateBuff(buff);

            healHPValue = HPManaPersenOfFOCFinal * player.GetFOC();
            healManaValue = HPManaPersenOfFOCFinal * player.GetFOC();

            debuffATK = new Buff(
                    this.id + "atk",
                    this.Name,
                    BuffType.ATK,
                    atkPersenOfATKFinal * player.GetATK(),
                    Timer
                );
            debuffDEF = new Buff(
                    this.id + "def",
                    this.Name,
                    BuffType.DEF,
                    defPersenOfDEFFinal * player.GetDEF(),
                    Timer
                );
            debuffSystem.ActivateDebuff(debuffATK);
            debuffSystem.ActivateDebuff(debuffDEF);
        }
    }

    public override void OnActivated(GameObject gameObject)
    {
        if (!buffSystem.CheckBuff(buff) || buffSystem.CheckBuff(BuffType.Idiosyncrasy))
        {
            Deactivate(gameObject);
            return;
        }
        else
        {
            if (harmonyTimer <= 0)
            {
                player.Heal(Stat.HP, healHPValue);
                player.Heal(Stat.Mana, healManaValue);

                harmonyTimer = 1;
            }

            harmonyTimer -= Time.deltaTime;
        }
    }

    public override void Deactivate(GameObject gameObject)
    {
        debuffSystem.DeactivateDebuff(debuffATK);
        debuffSystem.DeactivateDebuff(debuffDEF);
        animator.Play("holy_sonata_end");
    }
}