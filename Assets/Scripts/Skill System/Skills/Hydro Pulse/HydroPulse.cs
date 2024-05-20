using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Hydro Pulse")]
public class HydroPulse : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;
    [Header("Skill Range")]
    [SerializeField] public float radius;
    // [HideInInspector] public Transform ;

    public override float GetDamage(Player player)
    {
        return damage += dmgPersenOfATK * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        //  = GameObject.Find("Player").transform;


    }

    public override void OnActivated(GameObject gameObject)
    {
    }



}