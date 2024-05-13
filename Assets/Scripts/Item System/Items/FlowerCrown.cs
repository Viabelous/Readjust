using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Flower Crown")]
public class FlowerCrown : Item
{
    [Header("Buff Value")]
    [SerializeField] private float extraMaxHP;
    [SerializeField] private float extraMaxMana;

    private ItemSystem itemSystem;

    public override void Activate(GameObject player)
    {
        itemSystem = player.GetComponent<ItemSystem>();
        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!
        if (itemSystem.CheckItem("Badge of Honour"))
        {
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.player.Upgrade(Stat.HP, extraMaxHP);
        playerController.player.Upgrade(Stat.Mana, extraMaxHP);
    }

}