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
public class RewardMultiplier : Item
{
    [Header("Reward")]
    [SerializeField] public RewardType rewardType;
    [SerializeField] private float persentase;
    [HideInInspector] public float result;
    // private BuffSystem buffSystem;

    public override void Activate(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        switch (rewardType)
        {
            case RewardType.Aerus:
                result = persentase * playerController.player.aerus;
                // playerController.player.Collect(RewardType.Aerus, );
                break;
            case RewardType.ExpOrb:
                result = persentase * playerController.player.aerus;
                // playerController.player.Collect(RewardType.ExpOrb, persentase * playerController.player.exp);
                break;
            case RewardType.Venetia:
                result = persentase * playerController.player.aerus;
                // playerController.player.Collect(RewardType.Venetia, persentase * playerController.player.venetia);
                break;
            case RewardType.Score:
                result = persentase * StageManager.instance.score;
                break;
        }
    }

}