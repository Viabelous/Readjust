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

    public override float GetDamage(Character character)
    {
        return damage += dmgPersenOfDEF * character.def + dmgPersenOfATK * character.atk;
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }
}