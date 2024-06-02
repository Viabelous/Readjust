using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Stat Booster")]
public class StatBooster : Item
{
    private BuffSystem buffSystem;
    // private Buff buff;

    [Header("Buff Effect")]
    [SerializeField] private List<float> value = new List<float>();

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
                value[i],
                0
            );

            buffSystem.ActivateBuff(buff);

        }
    }
}