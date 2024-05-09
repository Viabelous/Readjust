using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherworldlyAcorn : Item
{
    private Item item;
    private BuffSystem buffSystem;
    private Buff buff;

    private float value;
    private void Activate()
    {
        item = item.Clone();
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        buff = new Buff(
            item.Id,
            item.Name,
            item.Type,
            value,
            0
        );

        buffSystem.ActivateBuff(buff);

        // Destroy(gameObject);
    }
}