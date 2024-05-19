using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stalactite Shoot")]
public class StalactiteShoot : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfDEF;
    [SerializeField] private float dmgPersenOfATK;

    public float dmgPersenOfDEFFinal
    {
        get { return dmgPersenOfDEF + 0.2f * (level - 1); }
    }

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Menembakkan tanah buatan runcing ke arah depan dan belakang yang akan mengakibatkan earth damage sebesar 20 + " + dmgPersenOfDEFFinal * 100 + "% DEF + " + dmgPersenOfATKFinal * 100 + "% ATK pada musuh yang terkena serangan.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage += dmgPersenOfDEFFinal * player.GetDEF() + dmgPersenOfATKFinal * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }
}