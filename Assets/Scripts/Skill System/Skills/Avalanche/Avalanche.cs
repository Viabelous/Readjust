using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Avalanche")]
public class Avalanche : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfATKUp;

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + dmgPersenOfATKUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionATK = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfATKFinal - dmgPersenOfATK) + "%) " : " ";

        description = "Menyerang musuh menggunakan hentakan tanah, mengakibatkan earth damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK kepada semua musuh di hadapan karakter dengan jarak kecil.";
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