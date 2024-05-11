using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/FoodDeliveryToken")]
public class FoodDeliveryToken : Item
{
    [Header("Foods Sprite")]
    [SerializeField] private GameObject food;
    [SerializeField] private float healHP;
    private float maxTimer;
    [SerializeField] private float timer;
    private BuffSystem buffSystem;


    public override void Activate(GameObject player)
    {
        maxTimer = timer;
        buffSystem = player.GetComponent<BuffSystem>();
        Buff buff = new Buff(this.id, this.name, BuffType.Custom, 0, 0);
        buffSystem.ActivateBuff(buff);

        food.GetComponent<FoodDeliveryBehaviour>().healHP = healHP;
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
        // float x, y, z;
        // x = UnityEngine.Random.Range(-10, 10);
        // y = 13;
        // z = 0;

        // Vector3 randomPos = new Vector3(x, y, z);
        Instantiate(food);
    }
}