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
            if (types[i] == BuffType.Custom)
            {
                continue;
            }

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

    public override void Adapting(Map map)
    {
        switch (map)
        {
            case Map.Stage1:
                types[0] = BuffType.Fire;
                break;
            case Map.Stage2:
                types[0] = BuffType.Earth;
                break;
            case Map.Stage3:
                types[0] = BuffType.Water;
                break;
            case Map.Stage4:
                types[0] = BuffType.Air;
                break;
            case Map.Stage5:
                types[0] = BuffType.Custom;
                break;
        }
    }
}