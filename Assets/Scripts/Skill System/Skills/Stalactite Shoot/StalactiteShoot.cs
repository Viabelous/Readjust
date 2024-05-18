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

    public override float GetDamage(Player player)
    {
        return damage += dmgPersenOfDEF * player.GetDEF() + dmgPersenOfATK * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }
}