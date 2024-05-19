using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Avalanche")]
public class Avalanche : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;
    // private PlayerController playerController;
    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Menyerang musuh menggunakan hentakan tanah, mengakibatkan earth damage sebesar 40 + " + dmgPersenOfATKFinal * 100 + "% ATK kepada semua musuh di hadapan karakter dengan jarak kecil.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfATKFinal * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }



}