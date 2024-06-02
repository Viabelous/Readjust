using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Holy Sonata")]
public class HolySonata : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float HPManaPersenOfFOC;
    // [SerializeField] private float manaPersenOfFOC;

    [Header("Debuff Value")]
    [SerializeField] private float ATKPersenOfATK;
    [SerializeField] private float DEFPersenOfDEF;

    [Header("Level Up Value")]
    [SerializeField] private float HPManaPersenOfFOCUp;
    [SerializeField] private float ATKPersenOfATKUp;
    [SerializeField] private float DEFPersenOfDEFUp;

    private float healHPValue, healManaValue, harmonyTimer;
    private BuffSystem buffSystem;
    private DebuffSystem debuffSystem;
    private Animator animator;
    private Player player;
    private Buff buff, debuffATK, debuffDEF;

    public float HPManaPersenOfFOCFinal
    {
        get { return HPManaPersenOfFOC + HPManaPersenOfFOCUp * (level - 1); }
    }

    public float ATKPersenOfATKFinal
    {
        get { return ATKPersenOfATK + ATKPersenOfATKUp * (level - 1); }
    }
    public float DEFPersenOfDEFFinal
    {
        get { return DEFPersenOfDEF + DEFPersenOfDEFUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionHPMana = level > 1 ? " (+" + PersentaseToInt(HPManaPersenOfFOCFinal - HPManaPersenOfFOC) + "%) " : " ";
        string additionATK = level > 1 ? " (" + PersentaseToInt(ATKPersenOfATKFinal - ATKPersenOfATK) + "%) " : " ";
        string additionDEF = level > 1 ? " (" + PersentaseToInt(DEFPersenOfDEFFinal - DEFPersenOfDEF) + "%) " : " ";

        description = "Memberikan status {Harmony} pada karakter yang akan terus mengisi HP dan Mana karakter sebanyak " + PersentaseToInt(HPManaPersenOfFOC) + "%" + additionHPMana + "FOC setiap detik namun menurunkan ATK serta DEF karakter masing-masing sebanyak " + PersentaseToInt(ATKPersenOfATK) + "%" + additionATK + "ATK dan " + PersentaseToInt(DEFPersenOfDEF) + "%" + additionDEF + "DEF. Menggunakan kembali skill ini saat sedang memiliki status {Harmony} tidak akan mengurangi Mana dan akan menonaktifkan status {Harmony}. Menggunakan skill ini akan menghapus status {Idiosyncrasy}.";
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
                    ATKPersenOfATKFinal * player.GetATK(),
                    Timer
                );
            debuffDEF = new Buff(
                    this.id + "def",
                    this.Name,
                    BuffType.DEF,
                    DEFPersenOfDEFFinal * player.GetDEF(),
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
                // Debug.Log("healHP: " + healHPValue);
                // Debug.Log("healMana: " + healManaValue);
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