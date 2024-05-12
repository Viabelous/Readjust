using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    Aerus,
    ExpOrb,
    Venetia,
    Score
}

[CreateAssetMenu(menuName = "Item/Multiply Reward")]
public class MultiplyReward : Item
{
    [Header("Reward")]
    [SerializeField] public RewardType rewardType;
    [SerializeField] private float persentase;
    [HideInInspector] public float result;
    private BuffSystem buffSystem;

    public override void Activate(GameObject player)
    {
        buffSystem = player.GetComponent<BuffSystem>();
        Buff buff = new Buff(this.id, this.name, BuffType.Custom, 0, 0);
        buffSystem.ActivateBuff(buff);

        PlayerController playerController = player.GetComponent<PlayerController>();
        switch (rewardType)
        {
            case RewardType.Aerus:
                result = persentase * playerController.player.aerus;
                break;
            case RewardType.ExpOrb:
                result = persentase * playerController.player.exp;
                break;
            case RewardType.Venetia:
                result = persentase * playerController.player.venetia;
                break;
            case RewardType.Score:
                result = persentase * StageManager.instance.score;
                break;
        }
    }

}