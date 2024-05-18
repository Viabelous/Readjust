using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Fireball")]
public class Fireball : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfATK * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }
}