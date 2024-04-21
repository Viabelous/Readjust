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

        if (playerController.hp < 0.01 * GameManager.player.maxHp)
        {
            Destroy(gameObject);
        }

        float result = persentase * playerController.hp;

        if (result > GameManager.player.maxMana)
        {
            playerController.mana = GameManager.player.maxMana;

        }
        else
        {
            playerController.mana += result;
        }

        playerController.UpdateManaBar();

        playerController.hp -= result;
        playerController.UpdateHealthBar();

        gameObject.transform.position = player.transform.position;
        // gameObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);


    }


}
