using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sacrivert : Skill
{
    private PlayerController playerController;
    public float persentase;

    public override void Activate(GameObject gameObject)
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        float result = persentase * playerController.player.hp;

        if (result > playerController.player.maxMana)
        {
            playerController.player.mana = playerController.player.maxMana;
        }
        else
        {
            playerController.player.mana += result;
        }

    }


}
