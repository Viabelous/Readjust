using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sacrivert : Skill
{
    private GameObject player;
    private PlayerController playerController;
    public float persentase = 0.1f;
    public override void Activate(GameObject gameObject)
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        if (playerController.player.hp < 0.01 * playerController.player.maxHp)
        {
            Destroy(gameObject);
        }

        float result = persentase * playerController.player.hp;

        if (result > playerController.player.maxMana)
        {
            playerController.player.mana = playerController.player.maxMana;

        }
        else
        {
            playerController.player.mana += result;
        }

        playerController.player.hp -= result;

        gameObject.transform.position = player.transform.position;
        // gameObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);


    }


}
