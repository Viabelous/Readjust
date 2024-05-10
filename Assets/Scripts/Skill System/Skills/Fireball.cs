using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Fireball")]
public class Fireball : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    public override float GetDamage(Character character)
    {
        return damage + dmgPersenOfATK * character.atk;
    }

    public override void Activate(GameObject gameObject)
    {
        StageManager.instance.PlayerActivatesSkill(this);
    }
}