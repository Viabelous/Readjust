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

    private BuffSystem buffSystem;

    public override void Activate(GameObject player)
    {
        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!
        if (CumaBuatDebug.instance.selectedItems.FindIndex(item => item.Name == "Badge of Honour") != -1)
        {
            return;
        }


        buffSystem = player.GetComponent<BuffSystem>();
        Buff buff = new Buff(this.id, this.name, BuffType.Custom, 0, 0);
        buffSystem.ActivateBuff(buff);

        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.player.Upgrade(Stat.HP, extraMaxHP);
        playerController.player.Upgrade(Stat.Mana, extraMaxHP);
    }

}