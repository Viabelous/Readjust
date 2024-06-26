using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Thorn Cover")]
public class ThornCover : Skill
{

    [Header("Buff Value")]
    [SerializeField] private float dmgPersenOfDEF;
    [SerializeField] private float dmgPersenOfATK;
    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfDEFUp;
    [SerializeField] private float dmgPersenOfATKUp;
    [Header("Skill Effect")]
    private GameObject thornEffect;
    private BuffSystem buffSystem;
    private Buff buff;

    private float thornTimer = 0;

    public float dmgPersenOfDEFFinal
    {
        get { return dmgPersenOfDEF + dmgPersenOfDEFUp * (level - 1); }
    }

    public float dmgPersenOfDEFFinalPersen
    {
        get { return dmgPersenOfDEFFinal + 2.5f; }
    }


    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + dmgPersenOfATKUp * (level - 1); }
    }


    public float dmgPersenOfATKFinalPersen
    {
        get { return dmgPersenOfATKFinal + 1; }
    }


    public override string GetDescription()
    {
        string additionDEF = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfDEFFinal - dmgPersenOfDEF) + "%)" : " ";
        string additionATK = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfATKFinal - dmgPersenOfATK) + "%)" : " ";

        description = "Memberikan status {Thorny} pada karakter untuk beberapa waktu. Ketika musuh mengakibatkan damage dari sentuhan kepada karakter, musuh akan terkena damage sebesar " + PersentaseToInt(dmgPersenOfDEF) + "%" + additionDEF + "DEF + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        buffSystem = playerController.GetComponent<BuffSystem>();
        Payment(buffSystem.transform);

        float value = dmgPersenOfDEFFinalPersen * playerController.player.GetDEF() + dmgPersenOfATKFinalPersen * playerController.player.GetATK();
        buff = new Buff(
                this.id,
                this.name,
                BuffType.Thorn,
                value,
                this.timer
            );
        buffSystem.ActivateBuff(buff);
    }

    public override void OnActivated(GameObject gameObject)
    {
        thornTimer -= Time.deltaTime;
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetThornEffect()
    {
        return thornEffect;
    }

    public override void WhileHitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (thornTimer <= 0)
            {
                Instantiate(thornEffect, buffSystem.transform);
            }
            else
            {
                thornTimer = 1;
            }
        }
    }
}
