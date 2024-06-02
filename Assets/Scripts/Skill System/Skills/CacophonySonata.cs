using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Cacophony Sonata")]
public class CacophonySonata : Skill
{
    [Header("Debuff Value")]
    [SerializeField] private float HPManaPersenOfFOC;

    [Header("Buff Value")]
    [SerializeField] private float ATKPersenOfFOC;

    [Header("Level Up Value")]
    [SerializeField] private float HPManaPersenOfFOCUp;
    [SerializeField] private float ATKPersenOfFOCUp;

    private float hpValue, manaValue, idiosyncrasyTimer;
    private BuffSystem buffSystem;
    private Animator animator;
    private Player player;
    private Buff buff, buffATK;

    public float HPManaPersenOfFOCFinal
    {
        get { return HPManaPersenOfFOC + HPManaPersenOfFOCUp * (level - 1); }
    }

    public float ATKPersenOfFOCFinal
    {
        get { return ATKPersenOfFOC + ATKPersenOfFOCUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionHPMana = level > 1 ? " (" + PersentaseToInt(HPManaPersenOfFOCFinal - HPManaPersenOfFOC) + "%) " : " ";
        string additionATK = level > 1 ? " +" + PersentaseToInt(ATKPersenOfFOCFinal - ATKPersenOfFOC) + "%) " : " ";

        description = "Memberikan status {Idiosyncrasy} pada karakter yang akan terus menguras HP dan Mana karakter sebanyak " + PersentaseToInt(HPManaPersenOfFOC) + "%" + additionHPMana + "FOC setiap detik namun meningkatkan drastis ATK karakter sebanyak " + PersentaseToInt(ATKPersenOfFOC) + "%" + additionATK + " FOC.  Menggunakan kembali skill ini saat sedang memiliki status {Idiosyncrasy} tidak akan mengurangi Mana dan akan menonaktifkan status {Idiosyncrasy}. Menggunakan skill ini akan menghapus status {Harmony}.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        idiosyncrasyTimer = 0;
        animator = gameObject.GetComponent<Animator>();
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        player = buffSystem.GetComponent<PlayerController>().player;

        if (buffSystem.CheckBuff(BuffType.Idiosyncrasy))
        {
            // tidak gunakan mana, tapi memakan cooldown
            StartCooldown();
            buffSystem.DeactivateBuff(BuffType.Idiosyncrasy);
            Destroy(gameObject);// hapus gameobject yg sebagai trigger
        }
        else
        {
            if (buffSystem.CheckBuff(BuffType.Harmony))
            {
                buffSystem.DeactivateAllRelatedBuff(
                    buffSystem.GetBuffNameOfType(BuffType.Harmony)
                );
            }

            // gunakan mana, tapi tidak memakan cooldown
            PayWithCostType(buffSystem.GetComponent<PlayerController>().player);
            // Payment(buffSystem.transform);

            manaValue = HPManaPersenOfFOCFinal * player.GetFOC();
            hpValue = HPManaPersenOfFOCFinal * player.GetFOC();

            buff = new Buff(
                    this.id,
                    this.Name,
                    BuffType.Idiosyncrasy,
                    0,
                    this.timer
                );

            buffATK = new Buff(
                    this.id + "atk",
                    this.Name,
                    BuffType.ATK,
                    ATKPersenOfFOC * player.GetFOC(),
                    this.timer
                );

            buffSystem.ActivateBuff(buff);
            buffSystem.ActivateBuff(buffATK);
        }
    }

    public override void OnActivated(GameObject gameObject)
    {

        if (!buffSystem.CheckBuff(buff))
        {
            Deactivate(gameObject);
            return;
        }
        else
        {
            if (idiosyncrasyTimer <= 0)
            {
                player.Pay(CostType.Hp, hpValue);
                player.Pay(CostType.Mana, manaValue);
                idiosyncrasyTimer = 1;
            }

            idiosyncrasyTimer -= Time.deltaTime;
        }
    }

    public override void Deactivate(GameObject gameObject)
    {
        buffSystem.DeactivateBuff(buffATK);
        animator.Play("cacophony_sonata_end");
    }
}