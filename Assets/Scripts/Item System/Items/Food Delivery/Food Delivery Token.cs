using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Food Delivery Token")]
public class FoodDeliveryToken : Item
{
    [Header("Food Sprite")]
    [SerializeField] private GameObject food;
    [Header("Heal Value")]
    [SerializeField] private float healHP;
    [SerializeField] private float healMana;
    [Header("Time Interval")]
    [SerializeField] private float timer;
    private float maxTimer;
    private BuffSystem buffSystem;



    public override void Activate(GameObject player)
    {
        maxTimer = timer;
        buffSystem = player.GetComponent<BuffSystem>();
        Buff buff = new Buff(this.id, this.name, BuffType.Custom, 0, 0);
        buffSystem.ActivateBuff(buff);

        food.GetComponent<FoodDeliveryBehaviour>().healHP = healHP;
        food.GetComponent<FoodDeliveryBehaviour>().healMana = healMana;
    }

    public override void OnActivated(GameObject player)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            DropFood();
            timer = maxTimer;
        }
    }

    private void DropFood()
    {
        Instantiate(food);
    }
}