using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/A Breeze Being Told")]
public class ABreezeBeingTold : Skill
{
    [Header("Custom Timer")]
    [SerializeField] private float timerPersenOfAGI;
    [Header("Skill Effect")]
    [SerializeField] private float dmgPersenOfTotalDmg;
    [SerializeField] private float HPPersenOfAGI;
    private PlayerController playerController;
    private BuffSystem buffSystem;
    private Buff buff;

    public float dmgPersenOfTotalDmgFinal
    {
        get { return dmgPersenOfTotalDmg + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Memanggil {Breezewheel} yang akan menyebabkan damage sebesar " + dmgPersenOfTotalDmgFinal * 100 + "% damage kepada semua musuh ketika diberi damage oleh karakter. Ketika terdapat musuh dengan status {Bloodlink} di stage, damage yang diakibatkan oleh skill ini akan berkurang setengah. Ketika terdapat status {Harmony} atau {Idiosyncrasy} pada karakter, akan memulihkan HP sebanyak " + HPPersenOfAGI * 100 + "% AGI ketika pemanggilannya. {Breezewheel} akan bertahan di stage selama " + timerPersenOfAGI * 100 + "% AGI detik.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        this.timer = timerPersenOfAGI * playerController.player.GetAGI();


        Payment(playerController.transform);

        buffSystem = playerController.GetComponent<BuffSystem>();
        buff = new Buff(
                this.id,
                this.name,
                BuffType.Breezewheel,
                dmgPersenOfTotalDmgFinal,
                this.timer
            );
        buffSystem.ActivateBuff(buff);

        if (buffSystem.CheckBuff(BuffType.Harmony) || buffSystem.CheckBuff(BuffType.Idiosyncrasy))
        {
            buffSystem.ActivateBuff(
                new Buff(
                    this.id + "hp",
                    this.name,
                    BuffType.HP,
                    HPPersenOfAGI * playerController.player.GetAGI(),
                    0
                )
            );
        }
    }
}