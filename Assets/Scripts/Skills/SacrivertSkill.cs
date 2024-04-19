using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrivertSkill : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public float persentase = 0.1f;
    private bool isInstantiate = true;

    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        if (playerController.hp < 0.01 * GameManager.player.maxHp)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isInstantiate)
        {
            isInstantiate = false;
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
        }

        transform.position = player.transform.position;
    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
