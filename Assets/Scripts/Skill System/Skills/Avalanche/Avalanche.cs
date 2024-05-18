using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Avalanche")]
public class Avalanche : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;
    // private PlayerController playerController;

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfATK * player.GetATK();
    }


}