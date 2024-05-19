using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Ignite")]
public class Ignite : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + 0.2f * (level - 1); }
    }
    public override string GetDescription()
    {
        description = "Melakukan serangan tebasan dengan area luas yang dapat mengakibatkan damage tinggi sebesar 150 + " + dmgPersenOfATKFinal * 100 + "% ATK ke musuh yang terkena serangan.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return this.damage + dmgPersenOfATKFinal * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }

}