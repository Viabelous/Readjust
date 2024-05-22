using System.Collections;
using System.Collections.Generic;
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
    private BuffSystem buffSystem;
    private Buff buff;

    public float dmgPersenOfDEFFinal
    {
        get { return dmgPersenOfDEF + dmgPersenOfDEFUp * (level - 1); }
    }

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + dmgPersenOfATKUp * (level - 1); }
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

        float value = dmgPersenOfDEFFinal * playerController.player.GetDEF() + dmgPersenOfATKFinal * playerController.player.GetATK();
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
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }
}
