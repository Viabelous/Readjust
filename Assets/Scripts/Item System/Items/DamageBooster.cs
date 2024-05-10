using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Damage Booster")]
public class DamageBooster : Item
{
    private BuffSystem buffSystem;
    // private Buff buff;

    [Header("Buff Effect")]
    [SerializeField] private List<float> persentase = new List<float>();

    public override void Activate(GameObject player)
    {
        buffSystem = player.GetComponent<BuffSystem>();

        // meningkatkan atk / def / agi / foc
        for (int i = 0; i < types.Count; i++)
        {
            Buff buff = new Buff(
                id,
                name,
                types[i],
                persentase[i],
                0
            );

            buffSystem.ActivateBuff(buff);

        }
    }
}